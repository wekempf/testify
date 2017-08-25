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
 *  Push - Test
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

#tool nuget:?package=vswhere
#tool nuget:?package=xunit.runner.console
#tool nuget:?package=OpenCover
#tool nuget:?package=ReportGenerator
#tool nuget:?package=GitVersion.CommandLine
#tool coveralls.net
#tool docfx.console
#tool "nuget:?package=GitReleaseNotes"

#addin Cake.Coveralls
#addin nuget:?package=Cake.Git
#addin Cake.DocFx
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
var gitPagesBranch = "gh-pages";
var branch = EnvironmentVariable("APPVEYOR_REPO_BRANCH") ?? GitBranchCurrent(".").FriendlyName;
var vsLatest = VSWhereLatest();
var msBuildPath = GetFiles(vsLatest + "/**/bin/msbuild.exe").First().FullPath;
Information("Branch: " + branch);
Information("Visual Studio: " + vsLatest);
Information("MSBuild: " + msBuildPath);
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
    var settings = GetMSBuildSettings();
    Information(settings.ToolPath);
    settings.Targets.Add("Restore");
    foreach (var sln in solutions) {
        MSBuild(sln, settings);
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
    var settings = GetMSBuildSettings();
    foreach (var sln in solutions) {
        MSBuild(sln, settings);
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    EnsureDirectoryExists(testResultsDir);
    var projects = GetFiles("src/**/*.Tests.csproj");
    foreach (var project in projects)
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true
        };
        OpenCover(
            tool => tool.DotNetCoreTest(project.FullPath, settings),
            coverageFile,
            new OpenCoverSettings
            {
                ReturnTargetCodeOffset = 0,
                OldStyle = true,
                Register = "user",
                MergeOutput = FileExists(coverageFile)
            }
            .WithFilter("+[*]*")
            .WithFilter("-[*.Tests]*")
            .WithFilter("-[xunit.*]*"));
    }
    if (FileExists(coverageFile)) {
        if (isRunningOnBuildServer) {
            var coverallsSettings = new CoverallsNetSettings {
                RepoToken = EnvironmentVariable("CoverallsToken")
            };
            CoverallsNet(coverageFile, CoverallsNetReportType.OpenCover, coverallsSettings);
        } else {
            ReportGenerator(coverageFile, testResultsDir);
        }
    }
});

Task("Push")
    .IsDependentOn("Test")
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
    var packages = GetFiles("src/**/*.nupkg");
    foreach (var package in packages) {
        Information("Pushing package " + package.FullPath + ".");
    }
    NuGetPush(packages, settings);
});

Task("Docs")
    .Does(() =>
{
    var settings = new DocFxBuildSettings {
        WorkingDirectory = "./docs"
    };
    DocFxBuild(settings);
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
    .IsDependentOn("Push")
    .IsDependentOn("Docs");

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

MSBuildSettings GetMSBuildSettings() {
    var settings = new MSBuildSettings
    {
        ToolPath = msBuildPath,
        Configuration = configuration,
    };
    if (settings.Properties == null) {
        Information("Null properties.");
    }
    if (version != null) {
        settings.Properties.Add("AssemblyVersion", new[] { version.AssemblySemVer });
        settings.Properties.Add("FileVersion", new[] { version.MajorMinorPatch + ".0" });
        settings.Properties.Add("Version", new[] { version.NuGetVersion });
    }
    return settings;
}