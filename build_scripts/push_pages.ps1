[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$BuildFolder,
    
    [Parameter(Mandatory=$true)]
    [string]$PersonalAccessToken,

    [string]$Email,
    
    [string]$Username
    
)

pushd $BuildFolder
try {
    if (-not (git config --global --get user.email)) {
        if (-not ($PsBoundParameters.ContainsKey('Email'))) {
            throw "Email not specified"
        }
        Write-Host "- Set user.email config setting..."
        git config --global user.email $Email
    }
    if (-not (git config --global --get user.name)) {
        if (-not ($PsBoundParameters.ContainsKey('Username'))) {
            throw "Username not specified"
        }
        Write-Host "- Set user.name config setting..."
        git config --global user.name $Username
    }
    if (-not (git config --global --get push.default)) {
        Write-Host "- Set push.default config setting..."
        git config --global push.default matching
    }
    $autocrlf = git config --global --get core.autocrlf
    git config --global core.autocrlf false
    $Username = git config --global --get user.name

    Write-Host "- Building documentation..."
    if (-not ($env:Path -contains "$BuildFolder\docfx")) {
        $env:Path += ";$BuildFolder\docfx"
    }
    cd "$BuildFolder\docs"
    docfx metadata
    docfx build

    Write-Host "- Clone gh-pages branch..."
    cd "$BuildFolder\..\"
    git clone --quiet --branch=gh-pages "https://wekempf:$($PersonalAccessToken)@github.com/wekempf/testify.git" gh-pages
    cd gh-pages
    git status

    Write-Host "- Clean gh-pages folder..."
    Get-ChildItem -Attributes !r | Remove-Item -Recurse -Force

    Write-Host "- Copy contents of _site folder into gh-pages folder..."
    Copy-Item -path "$BuildFolder\docs\_site\*" -Destination $pwd.Path -Recurse

    git status
    $thereAreChanges = git status | select-string -pattern "Changes not staged for commit:","Untracked files:" -simplematch
    if ($thereAreChanges -ne $null) { 
        Write-Host "- Committing changes to documentation..."
        git add .
        git status
        git commit -m "skip ci - static site regeneration"
        git status
        Write-Host "- Push gh-pages..."
        git push --quiet
        Write-Host "- Pushed!"
    } 
    else { 
        write-host "- No changes to documentation to commit"
    }
} finally {
    git config --global core.autocrlf $autocrlf
    popd
}