[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder
)

pushd "$BuildFolder\docs"
try {
    Write-Host "- Building documentation..."
    if (-not ($env:Path -contains "$BuildFolder\docfx")) {
        $env:Path += ";$BuildFolder\docfx"
    }
    docfx metadata
    docfx build
    7z a "Site.zip" "_site"
} finally {
    popd
}