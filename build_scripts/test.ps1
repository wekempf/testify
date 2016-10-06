[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder
)

pushd $BuildFolder
try {
    $configuration = $env:CONFIGURATION
    if (-not $configuration) {
        $configuration = "Release"
    }
    $openCover = Resolve-Path "${BuildFolder}\packages\OpenCover.*\tools\OpenCover.Console.exe"
    $xunitRunner = Resolve-Path "${BuildFolder}\packages\xunit.runner.console.*\tools\xunit.console.exe"
    $testAssemblies = Get-ChildItem -Path $BuildFolder -Include *.Tests.dll -Recurse | Where-Object { $_ -like "*\bin\${configuration}\*" }
    Write-Host "Discovered OpenCover at ${openCover}"
    Write-Host "Discovered Xunit console runner at ${xunitRunner}"
    Write-Host "Discovered test assemblies $($testAssemblies -join ',')"
    Write-Host ""
    Invoke-Expression "${openCover} -register:user -target:`"${xunitRunner}`" -targetargs:`"${testAssemblies} -appveyor -noshadow`" -filter:`"+[*]*`" -output:opencoverCoverage.xml"
} finally {
    popd
} 