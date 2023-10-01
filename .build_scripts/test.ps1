[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder,

    [switch]$PublishResults
)

pushd $BuildFolder
try {
    $configuration = $env:CONFIGURATION
    if (-not $configuration) {
        $configuration = "Release"
    }
    $openCover = Resolve-Path "${BuildFolder}\packages\OpenCover.*\tools\OpenCover.Console.exe"
    $xunitRunner = Resolve-Path "${BuildFolder}\packages\xunit.runner.console.*\tools\xunit.console.exe"
    $coveralls = Resolve-Path "${BuildFolder}\packages\coveralls.net.*\tools\csmacnz.coveralls.exe"
    $testAssemblies = Get-ChildItem -Path $BuildFolder -Include *.Tests.dll -Recurse | Where-Object { $_ -like "*\bin\${configuration}\*" }
    $coverageFile = Join-Path $BuildFolder "opencoverCoverage.xml"
    Write-Host "Discovered OpenCover at ${openCover}"
    Write-Host "Discovered Xunit console runner at ${xunitRunner}"
    Write-Host "Discovered test assemblies $($testAssemblies -join ',')"
    $command = "${openCover} -register:user -target:`"${xunitRunner}`" -targetargs:`"${testAssemblies} -appveyor -noshadow`" -filter:`"+[*]* -[*]*.g.cs -[*.Tests]* -[xunit.*]*`" -output:${coverageFile}"
    Write-Host $command
    Invoke-Expression $command
    if ($PublishResults) {
        $command = "${coveralls} --opencover -i `"${coverageFile}`" --repoToken $env:CoverallsToken --commitId `"$env:APPVEYOR_REPO_COMMIT`" --commitBranch `"$env:APPVEYOR_REPO_BRANCH`" --commitAuthor `"$env:APPVEYOR_REPO_COMMIT_AUTHOR`" --commitEmail `"$env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL`" --commitMessage `"$env:APPVEYOR_REPO_COMMIT_MESSAGE`" --jobId `"$env:APPVEYOR_JOB_ID`""
        Write-Host $command
        Invoke-Expression $command
    }
} finally {
    popd
} 