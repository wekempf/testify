Push-Location $PsScriptRoot
try {
  Remove-Item api/testify.*,api/.manifest,_site -Recurse -Force -ErrorAction SilentlyContinue
} finally {
  Pop-Location
}