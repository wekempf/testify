param(
    [Parameter(Position = 0)]
    [string[]]$Tasks
)

. .build_helper.ps1

$script:BuildToolsPath = Join-Path $PSScriptRoot .buildtools
Write-Verbose "Build tools path: $BuildToolsPath"
if (-not ($env:PSModulePath -split [System.Io.Path]::PathSeparator | Where-Object { $_ -eq $script:BuildToolsPath })) {
    Write-Verbose 'Adding build tools path to PSModulePath.'
    $env:PSModulePath = $script:BuildToolsPath + [System.Io.Path]::PathSeparator + $env:PSModulePath
    Write-Verbose "PSModulePath: $env:PSModulePath"
}

if (-not (Get-Command Invoke-Build -ErrorAction SilentlyContinue)) {
    Write-Verbose 'Searching for InvokeBuild module.'
    if (-not (Get-Module InvokeBuild -ListAvailable)) {
        Write-Verbose 'InvokeBuild module not found. Installing from PSGallery.'
        Save-Module -Name InvokeBuild -Path $script:BuildToolsPath -Force
    }
    Write-Verbose 'Importing InvokeBuild module.'
    Import-Module InvokeBuild
}

# call the build engine with this script and return
if ($MyInvocation.ScriptName -notlike '*Invoke-Build.ps1') {
    Write-Verbose 'Calling Invoke-Build.'
    Invoke-Build $Tasks $MyInvocation.MyCommand.Path @PSBoundParameters
    return
}

Set-BuildHeader {
    param($Path)
    Section DarkYellow "Task $Path"
}

Set-BuildFooter {
    param($Path)
    Section DarkYellow "Done $Path, $($Task.Elapsed)"
}

task restore {
    dotnet restore Testify.sln --no-cache --force --verbosity quiet
}

task build restore, {
    dotnet build Testify.sln -c Release --no-restore --nologo
}

task test build, {
    dotnet test Testify.sln -c Release --no-build --no-restore --nologo
}

# Synopsis: Default build task.
task . build