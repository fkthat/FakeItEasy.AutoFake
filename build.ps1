[CmdletBinding()]
param(
    [Parameter(Position = 0)]
    [ValidateSet('clean', 'restore', 'build', 'test', 'coverage', 'pack')]
    $Target = 'build',
    [Parameter()]
    [ValidateSet('Debug', 'Release')]
    $Config = 'Debug'
)

Push-Location $PSScriptRoot

try {
    switch($Target) {
        'clean' {
            git clean -dfx -e .vs -e .vscode
        }
        'restore' {
            dotnet restore
        }
        'build' {
            dotnet build -c $Config
        }
        'test' {
            dotnet test -c $Config
        }
        'coverage' {
            dotnet tool restore &&
            dotnet tool run reportgenerator -reports:**\coverage.cobertura.xml `
                -targetdir:.coverage && Start-Process '.coverage\index.htm'
        }
        'pack' {
            dotnet pack -c $Config -o '.build'
        }
    }
}
finally {
    Pop-Location
}
