function Verbose($Message) {
    if ($env:GITHUB_ACTIONS) {
        Write-Output "::debug::$Message"
    }
    else {
        Write-Verbose $Message
    }
}

function Section([System.ConsoleColor]$ForegroundColor, $Message) {
    Write-Build $ForegroundColor "$Message $('-' * (80 - $Message.Length))"
}

function Info($Message) {
    Write-Build Cyan $Message
}

function Warning($Message) {
    if ($env:GITHUB_ACTIONS) {
        Write-Output "::warning::$Message"
    }
    else {
        Write-Build Yellow $Message
    }
}

function Error($Message) {
    if ($env:GITHUB_ACTIONS) {
        Write-Output "::error::$Message"
    }
    else {
        Write-Build Red $Message
    }
}