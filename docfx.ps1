[CmdletBinding()]
param (
    [Parameter(ValueFromRemainingArguments=$True)]
    $DocFXArguments
)

if ($DocFXArguments[0] -eq '--serve') {
    $docfxJson = Get-Item .\docfx_project\docfx.json | Select-Object -ExpandProperty FullName
    Write-Verbose "Using $docfxJson"
    $DocFXArguments = @("$docfxJson") + $DocFXArguments
}

$docfx = Get-Command 'docfx.exe' -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Source
if (-not (Test-Path $docfx -ErrorAction SilentlyContinue)) {
    Write-Verbose 'Looking for DocFx in .tools/'
    $docfx = Get-ChildItem -Path .tools -Include docfx.exe -Recurse -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName
    if (-not (Test-Path $docfx -ErrorAction SilentlyContinue)) {
        Write-Verbose 'Installing DocFx in .tools/'
        New-Item .tools -ItemType Directory -Force | Out-Null
        $nuget = Get-Command 'nuget.exe' -ErrorAction SilentlyContinue | Select-Object -ExcludeProperty Source
        if (-not (Test-Path $nuget -ErrorAction SilentlyContinue)) {
            Write-Verbose 'Installing nuget.exe in .tools/'
            Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile '.tools/nuget.exe' -ErrorAction Stop | Out-Null
            $nuget = Get-ChildItem -Path .tools -Include nuget.exe -Recurse -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName
        }
        Write-Verbose "Using $nuget"
        (& $nuget install docfx.console -o .tools) | Out-Null
        $docfx = Get-ChildItem -Path .tools -Include docfx.exe -Recurse -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName
    }
}
Write-Verbose "Using $docfx"
& $docfx @DocFXArguments