[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder
)

pushd $BuildFolder
try {
    Write-Host "- Downloading docfx..."
    (new-object net.webclient).DownloadFile('https://github.com/dotnet/docfx/releases/download/v1.5.1/docfx.zip', "$BuildFolder\docfx.zip")
    Write-Host "- Unpacking docfx..."
    [void](7z e docfx.zip -o"$BuildFolder\docfx")
} finally {
    popd
}
