param(
    [Parameter(Position=0)]
    [ValidateSet('?', '.', 'clean', 'doc-build', 'doc-serve')]
    [string[]]$Tasks
)

# Bootstrap methods
function Update-Path {
    param(
        [Parameter(Position=0)]
        $Path
    )
    $Path = Resolve-Path $Path
    $found = ($env:Path -split ';') -contains $Path
    Write-Verbose "$Path found in system path: $found"
    if (-not $found) {
        $env:Path += ";$Path"
        Write-Verbose "Updated Path: $($env:Path)"
    }
}

function Use-Module {
    param(
        [Parameter(Position=0)]
        [string]$Name
    )
    
    Write-Verbose "Using module: $Name"
    if (-not (Get-Module $Name -ErrorAction SilentlyContinue)) {
        $tools = New-Item -Path .tools -ItemType Directory -Force | Select-Object -ExpandProperty FullName
        if (-not (($env:PSModulePath -split ';') -contains $tools)) {
            $env:PSModulePath += ";$tools"
            Write-Verbose "Updated PSModulePath: $($env:PSModulePath)"
        }
        Write-Verbose "Saving module to .tools"
        Save-Module -Name $Name -Path '.tools'
    }
    Import-Module $Name -ErrorAction Stop
}

function Use-NuGetTool {
    param(
        [Parameter(Position=0)]
        [string]$ToolName,

        [Parameter(Position=1)]
        [string]$PackageName
    )

    Write-Verbose "Using tool: $ToolName"
    if (-not (Get-Command $ToolName -ErrorAction SilentlyContinue)) {
        Write-Verbose "Saving NuGet package to .tools"
        New-Item -Path .tools -ItemType Directory -Force | Out-Null
        if (-not (Get-Command nuget.exe -ErrorAction SilentlyContinue)) {
            Write-Verbose 'Saving nuget.exe to .tools'
            Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile '.tools/nuget.exe' -ErrorAction Stop | Out-Null
            Update-Path .tools
        }
        Write-Verbose "Saving $PackageName to .tools"
        nuget install $PackageName -o .tools | Out-Null
        $toolPath = Get-ChildItem -Path .tools -Include $ToolName -Recurse -ErrorAction SilentlyContinue | Split-Path | Get-Item | Select-Object -ExpandProperty FullName
        Update-Path $toolPath
        Get-Command $ToolName -ErrorAction Stop | Out-Null
    }
}

Use-Module InvokeBuild
Use-Module Glob
Use-NugetTool docfx.exe docfx.console

# call the build engine with this script and return
if ($MyInvocation.ScriptName -notlike '*Invoke-Build.ps1') {
    Invoke-Build $Tasks $MyInvocation.MyCommand.Path @PSBoundParameters
    return
}

$docfxJson = '.\docs\docfx.json'

task clean {
    Get-ChildItem bin,obj,_site -Recurse -ErrorAction SilentlyContinue |
        ForEach-Object { (Resolve-Path $_ -Relative) -replace '\\', '/' } |
        Where-Object { -not $_.StartsWith('.tools/') } |
        ForEach-Object { remove $_ }

    # Cleanup the docs/api directory
    $filesToKeep = @('.gitignore', 'index.md', 'toc.yml') |
        ForEach-Object { (Resolve-Path (Join-Path './docs/api' $_) -Relative) -replace '\\', '/' }
    Get-ChildItem docs/api |
        ForEach-Object { (Resolve-Path $_ -Relative) -replace '\\', '/' } |
        Where-Object { -not ($filesToKeep -contains $_) } |
        ForEach-Object { Remove-Item $_ }
}

task doc-build {
    docfx $docfxJson
}

task doc-serve {
    docfx $docfxJson --serve
}

task . doc-build