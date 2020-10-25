[CmdletBinding()]
param (
    [Parameter(Position = 0)][ValidateSet('build', 'test', 'clean', 'pack')] $Target = 'build',
    [Parameter()][ValidateSet('Debug', 'Release')] $Config = 'Debug',
    [Parameter()] [Switch] $ShowCoverageReport = $false
)

function BreakOnFail($p) { & $p
    if($LASTEXITCODE -ne 0) { break }
}

if($Target -eq 'clean') {
    git clean -dfx -e .vs -e .vscode
    break
}

if($Target -eq 'build' -or $Target -eq 'test' -or $Target -eq 'pack') {
    BreakOnFail { dotnet restore }
    BreakOnFail { dotnet build --no-restore -c $Config }

    if($Target -eq 'test' -or $Target -eq 'pack') {
        BreakOnFail { dotnet test --no-build -c $Config }

        if($ShowCoverageReport) {
            BreakOnFail { dotnet tool restore }

            BreakOnFail { dotnet tool run reportgenerator `
                -reports:**\coverage.cobertura.xml -targetdir:.coverage }

            Start-Process '.coverage\index.htm'
        }

        if($Target -eq 'pack') {
            BreakOnFail { dotnet pack -c $Config -o '.build' --no-build }
        }
    }

    break
}
