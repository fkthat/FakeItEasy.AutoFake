[CmdletBinding()]
param (
    [Parameter(Position = 0)][ValidateSet('restore', 'build', 'test', 'clean', 'pack')] $Target = 'build',
    [Parameter()][ValidateSet('Debug', 'Release')] $Config = 'Debug',
    [Parameter()] [Switch] $ShowCoverageReport = $false
)

if($Target -eq 'clean') {
    git clean -dfx -e .vs -e .vscode
    break
}

if($Target -eq 'restore') {
    dotnet restore
    break
}

if($Target -eq 'build') {
    dotnet build -c $Config
    break
}

if($Target -eq 'test') {
    dotnet test -c $Config || break

    if($ShowCoverageReport) {
        dotnet tool restore &&
        dotnet tool run reportgenerator `
            -reports:**\coverage.cobertura.xml `
            -targetdir:.coverage &&
        Start-Process '.coverage\index.htm'
    }

    break
}

if($Target -eq 'pack') {
    dotnet test -c $Config &&
    dotnet pack -c $Config -o '.build' --no-build
    break
}
