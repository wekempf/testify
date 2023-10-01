[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder
)

pushd $BuildFolder
try {
    Write-Host "- Installing docfx..."
    cinst docfx -y
} finally {
    popd
}
