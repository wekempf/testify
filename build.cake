/*
 *  Tasks: Clean, NuGetRestore, Version, Build, Test, NuGetPack, NuGetPush, Docs, Default
 *
 *  Clean
 *    Cleans the directory structure of all build artifacts (including NuGet packages).
 *    Not configuration dependent.
 *  NuGetRestore
 *    Restores NuGet packages for all solutions.
 *  Version
 *    Obtains the version to use from GitVersion and updates AssemblyInfo.cs files if running
 *    on the build server.
 *  Build - Clean, NuGetRestore, Version
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

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solution = File("Testify.sln");
var isRunningOnBuildServer = AppVeyor.IsRunningOnAppVeyor;
var updateAssemblyInfo = HasArgument("updateassemblyinfo") || isRunningOnBuildServer;
var solutions = GetFiles("**/*.sln")
    .Except(GetFiles("**/packages/**/*.sln"), FilePathComparer.Default)
    .Except(GetFiles("**/tools/**/*.sln"), FilePathComparer.Default);
var testResultsDir = Directory("./TestResults");
var coverageFile = testResultsDir + File("coverage.xml");
var releaseNuGetSource = "https://www.nuget.org/api/v2/package";
var releaseNuGetApiKey = EnvironmentVariable("NuGetApiKey");
var unstableNuGetSource = "https://www.myget.org/F/wekempf/api/v2/package";
var unstableNuGetApiKey = EnvironmentVariable("MyGetApiKey");
var gitPagesRepo = "https://wekempf:" + EnvironmentVariable("GitHubPersonalAccessToken") + "@github.com/wekempf/testify.git";
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
        if (FileExists("./docs/site.zip")) {
            DeleteFile("./docs/site.zip");
        }
    });

Task("NuGetRestore")
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
    .IsDependentOn("NuGetRestore")
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
        var packages = GetFiles("**/*.nupkg")
            .Except(GetFiles("**/packages/**/*.nupkg"), FilePathComparer.Default)
            .Except(GetFiles("./tools/**/*.nupkg"), FilePathComparer.Default);
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
            if (branch == "master") {
                if (GitClonePages() != 0) {
                    throw new Exception("Unable to clone Pages.");
                }
                var dirs = GetDirectories("./pages/*")
                    .Except(GetDirectories("./pages/.git"), DirectoryPathComparer.Default);
                DeleteDirectories(dirs, true);
                var files = GetFiles("./pages/*");
                DeleteFiles(files);
                CopyFiles("./docs/_site/**/*", "./pages", true);
                if (GitCommitPages() != 0) {
                    throw new Exception("Unable to commit Pages.");
                }
                if (GitPushPages() != 0) {
                    throw new Exception("Unable to publish Pages.");
                }
            }
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

public class FilePathComparer : IEqualityComparer<FilePath>
{
    public bool Equals(FilePath x, FilePath y)
    {
        return string.Equals(x.FullPath, y.FullPath, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(FilePath x)
    {
        return x.FullPath.GetHashCode();
    }

    private static FilePathComparer instance = new FilePathComparer();
    public static FilePathComparer Default
    {
        get { return instance; }
    }
}

public class DirectoryPathComparer : IEqualityComparer<DirectoryPath>
{
    public bool Equals(DirectoryPath x, DirectoryPath y)
    {
        return string.Equals(x.FullPath, y.FullPath, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(DirectoryPath x)
    {
        return x.FullPath.GetHashCode();
    }

    private static DirectoryPathComparer instance = new DirectoryPathComparer();
    public static DirectoryPathComparer Default
    {
        get { return instance; }
    }
}
