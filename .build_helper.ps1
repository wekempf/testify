function Test-BuildServer {
    if ($env:GITHUB_ACTIONS) {
        return 'github'
    }

    return $null
}

function Verbose($Message) {
    switch (Test-BuildServer) {
        'github' {
            Write-Output "::debug::$Message"
        }
        default {
            Write-Verbose $Message
        }
    }
}

function Section([System.ConsoleColor]$ForegroundColor, $Message) {
    Write-Build $ForegroundColor "$Message $('-' * (80 - $Message.Length))"
}

function Info($Message) {
    Write-Build Cyan $Message
}

function Warning($Message) {
    switch (Test-BuildServer) {
        'github' {
            Write-Output "::warning::$Message"
        }
        default {
            Write-Build Yellow $Message
        }
    }
}

function Err($Message) {
    switch (Test-BuildServer) {
        'github' {
            Write-Output "::error::$Message"
        }
        default {
            Write-Build Red $Message
        }
    }
}