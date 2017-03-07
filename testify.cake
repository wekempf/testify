/*
 *  Tasks: Clean, Restore, Version, Build, Test, NuGetPack, NuGetPush, Docs, Default
 *
 *  Clean
 *    Cleans the directory structure of all build artifacts (including NuGet packages).
 *    Not configuration dependent.
 *  Restore
 *    Restores NuGet packages for all solutions.
 *  Version
 *    Obtains the version to use from GitVersion and updates AssemblyInfo.cs files if running
 *    on the build server.
 *  Build - Clean, Restore, Version
 *    Builds all solutions. The 'configuration' argument determines what configuration to build:
 *    if not supplied 'Release' is assumed.
 *  Test - Build
 *    Runs all unit tests obtaining code coverage and producing a report. The report is either
 *    a local report in the TestResults directory or if running on the build server is a report
 *    sent to Coveralls. On the build server this requires a CoverallsToken environment variable
 *    to be set with the security token to use when publishing.
 *  NuGetPack - Build
 *    Creates nupkg packages.
 *  NuGetPush - NuGetPack
 *    Pushes NuGet packages to a NuGet server. This target does nothing when not run on the
 *    build server. Builds on the master branch push to the official NuGet.org server while
 *    all other branches push to a MyGet server. This requires either a NuGetApiKey
 *    or MyGetApiKey environment variable to be set to the API key to use when
 *    publishing.
 *  Docs
 *    Builds the documentation and if on the build server and building the master branch,
 *    publishes the documentation to GitHub pages. This requires Git to be installed and
 *    in the PATH and for the server to have write access to the GitHub repository.
 *  Default - Test, NuGetPush, Docs
 *    Performs a complete build of everything. 
 */

#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
#tool "nuget:?package=GitVersion.CommandLine"
#tool coveralls.net
#tool docfx.console

#addin Cake.Coveralls
#addin nuget:?package=Cake.Git
#addin "Cake.DocFx"
#addin nuget:?package=Cake.Kudu

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solution = File("Testify.sln");
var isRunningOnBuildServer = AppVeyor.IsRunningOnAppVeyor;
var updateAssemblyInfo = HasArgument("updateassemblyinfo") || isRunningOnBuildServer;
var solutions = GetFiles("**/*.sln") - GetFiles("**/packages/**/*.sln") - GetFiles("**/tools/**/*.sln");
var testResultsDir = Directory("./TestResults");
var coverageFile = testResultsDir + File("coverage.xml");
var releaseNuGetSource = "https://www.nuget.org/api/v2/package";
var releaseNuGetApiKey = EnvironmentVariable("NuGetApiKey");
var unstableNuGetSource = "https://www.myget.org/F/wekempf/api/v2/package";
var unstableNuGetApiKey = EnvironmentVariable("MyGetApiKey");
var gitPagesRepo = "https://wekempf:" + EnvironmentVariable("GitHubPersonalAccessToken") + "@github.com/wekempf/testify.git";
var gitPagesRepo = "git@github.com:wekempf/testify.git";
var gitPagesBranch = "gh-pages";
var branch = EnvironmentVariable("APPVEYOR_REPO_BRANCH") ?? GitBranchCurrent(".").FriendlyName;
Information("Branch: " + branch);
GitVersion version = null;

Task("Clean")
    .Does(() =>
{
    CleanDirectories("**/bin");
    CleanDirectories("**/obj");
    CleanDirectory("packages");
    CleanDirectory("TestResults");
    CleanDirectory("./docs/_site");
    if (FileExists("./docs/site.zip")) {
        DeleteFile("./docs/site.zip");
    }
    var files = GetFiles("./docs/api/*") - GetFiles("./docs/api/index.md") - GetFiles("./docs/api/toc.yml");
    DeleteFiles(files);
});

Task("Restore")
    .Does(() =>
{
    foreach (var sln in solutions) {
        NuGetRestore(sln);
    }
});

Task("Version")
    .Does(() =>
{
    var settings = new GitVersionSettings
    {
        UpdateAssemblyInfo = updateAssemblyInfo,
    };
    version = GitVersion(settings);
    if (isRunningOnBuildServer) {
        settings.OutputType = GitVersionOutput.BuildServer;
        GitVersion(settings);
    }
    Information("Version: {0}", version.NuGetVersion);
    if (updateAssemblyInfo) {
        Information("Updated assembly information.");
    }
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Version")
    .Does(() =>
{
    var settings = new MSBuildSettings
    {
        Configuration = configuration
    };
    foreach (var sln in solutions) {
        MSBuild(sln, settings);
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var testAssemblies = GetFiles("**/bin/**/*.Tests.dll");
    EnsureDirectoryExists(testResultsDir);

    var settings = new XUnit2Settings
    {
        ShadowCopy = false
    };

    OpenCover(
        tool => tool.XUnit2(testAssemblies, settings),
        coverageFile,
        new OpenCoverSettings()
            .WithFilter("+[*]*")
            .WithFilter("-[*.Tests]*")
            .WithFilter("-[xunit.*]*"));

    if (isRunningOnBuildServer) {
        var coverallsSettings = new CoverallsNetSettings {
            RepoToken = EnvironmentVariable("CoverallsToken")
        };
        CoverallsNet(coverageFile, CoverallsNetReportType.OpenCover, coverallsSettings);
    } else {
        ReportGenerator(coverageFile, testResultsDir);
    }
});

Task("NuGetPack")
    .IsDependentOn("Build")
    .Does(() =>
{
    var outputDirectory = MakeAbsolute(Directory("./src/Testify/bin/" + configuration));
    var settings = new NuGetPackSettings
    {
        Id = "Testify",
        Version = version.NuGetVersion,
        Title = "Testify",
        Authors = new[] { "William E. Kempf" },
        Owners = new[] { "William E. Kempf" }, 
        Description = "Testify is a unit test assertions, test data creation and contract verification framework. It's not dependent on any specific unit testing framework.",
        Summary = "Testify is a unit test assertions, test data creation and contract verification framework. It's not dependent on any specific unit testing framework.",
        ProjectUrl = new Uri("http://wekempf.github.io/testify/"),
        IconUrl = new Uri("https://raw.githubusercontent.com/wekempf/testify/develop/docs/images/gavel_icon-64x64.png"),
        LicenseUrl = new Uri("https://raw.githubusercontent.com/wekempf/testify/master/LICENSE"),
        Copyright = "Copyright " + DateTime.Now.Year.ToString(),
        Tags = new [] { "test", "unit", "testing", "TDD", "AAA", "assert", "assertion", "factory", "verifier" },
        RequireLicenseAcceptance = false,
        Symbols = false,
        Files = new [] {
            new NuSpecContent { Source = "Testify.dll", Target = "lib/portable45-net45+win8+wpa81" },
            new NuSpecContent { Source = "Testify.xml", Target = "lib/portable45-net45+win8+wpa81" }
        },
        BasePath = outputDirectory,
        OutputDirectory = outputDirectory
    };
    NuGetPack(settings);

    outputDirectory = MakeAbsolute(Directory("./src/Testify.Moq/bin/" + configuration));
    settings = new NuGetPackSettings
    {
        Id = "Testify.Moq",
        Version = version.NuGetVersion,
        Title = "Testify.Moq",
        Authors = new[] { "William E. Kempf" },
        Owners = new[] { "William E. Kempf" }, 
        Description = "Testify is a unit test assertions, test data creation and contract verification framework. It's not dependent on any specific unit testing framework.",
        Summary = "Testify is a unit test assertions, test data creation and contract verification framework. It's not dependent on any specific unit testing framework. This extension adds auto mocking support using Moq.",
        ProjectUrl = new Uri("http://wekempf.github.io/testify/"),
        IconUrl = new Uri("https://raw.githubusercontent.com/wekempf/testify/develop/docs/images/gavel_icon-64x64.png"),
        LicenseUrl = new Uri("https://raw.githubusercontent.com/wekempf/testify/master/LICENSE"),
        Copyright = "Copyright " + DateTime.Now.Year.ToString(),
        Tags = new [] { "test", "unit", "testing", "TDD", "AAA", "assert", "assertion", "factory", "verifier" },
        RequireLicenseAcceptance = false,
        Symbols = false,
        Files = new [] {
            new NuSpecContent { Source = "Testify.Moq.dll", Target = "lib/net45" },
            new NuSpecContent { Source = "Testify.Moq.xml", Target = "lib/net45" }
        },
        Dependencies = new[] {
            new NuSpecDependency { Id = "Testify", Version = version.NuGetVersion },
            new NuSpecDependency { Id = "Moq", Version = "4.5" }
        },
        BasePath = outputDirectory,
        OutputDirectory = outputDirectory
    };
    NuGetPack(settings);
});

Task("NuGetPush")
    .IsDependentOn("NuGetPack")
    .Does(() =>
{
    if (!isRunningOnBuildServer) {
        Warning("Not running on build server. Skipping");
        return;
    }

    NuGetPushSettings settings = null;
    if (branch == "master") {
        Information("Pushing release package.");
        settings = new NuGetPushSettings {
            Source = releaseNuGetSource,
            ApiKey = releaseNuGetApiKey
        };
    } else {
        Information("Pushing unstable package.");
        settings = new NuGetPushSettings {
            Source = unstableNuGetSource,
            ApiKey = unstableNuGetApiKey
        };
    }
    Information("Pushing to " + settings.Source + ".");
    var packages = GetFiles("**/*.nupkg") - GetFiles("**/packages/**/*.nupkg") - GetFiles("./tools/**/*.nupkg");
    foreach (var package in packages) {
        Information("Pushing package " + package.FullPath + ".");
    }
    NuGetPush(packages, settings);
});

Task("Docs")
    .Does(() =>
{
    var settings = new DocFxSettings {
        WorkingDirectory = "./docs"
    };
    DocFx(settings);
    Zip("./docs/_site", "./docs/site.zip");
    if (isRunningOnBuildServer) {
        //if (branch == "master") {
            GitClone(gitPagesRepo, "./pages", new GitCloneSettings { BranchName = gitPagesBranch });
            Information("Sync output files...");
            Kudu.Sync("./docs/_site", "./pages", new KuduSyncSettings {
                ArgumentCustomization = args => args.Append("--ignore").AppendQuoted(".git;CNAME")
            });
            Information("Stage all changes...");
            GitAddAll("./pages");
            Information("Commit all changes...");
            var sourceCommit = GitLogTip("./");
            GitCommit(
                "./pages",
                sourceCommit.Committer.Name,
                sourceCommit.Committer.Email,
                string.Format("AppVeyor Publish: {0}\r\n{1}", sourceCommit.Sha, sourceCommit.Message));
            Information("Publishing all changes...");
            GitPush("./page");
       //}
    }
});


Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("Docs")
    .IsDependentOn("NuGetPush");

RunTarget(target);

int GitClonePages() {
    return StartProcess("git", new ProcessSettings {
        Arguments = "clone " + gitPagesRepo + " -b " + gitPagesBranch + " pages" 
    });
}

int GitCommitPages() {
    var result = StartProcess("git", new ProcessSettings {
        Arguments = "add .",
        WorkingDirectory = "./pages"
    });
    if (result != 0) {
        return result;
    }
    result = StartProcess("git", new ProcessSettings {
        Arguments = "commit -m \"Publishing pages " + version.AssemblySemVer + "\"",
        WorkingDirectory = "./pages"
    });
    return result;
}

int GitPushPages() {
    return StartProcess("git", new ProcessSettings {
        Arguments = "push",
        WorkingDirectory = "./pages"
    });
}
