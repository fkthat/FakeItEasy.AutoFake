[CmdletBinding()]
param(
    [Parameter(Position = 0, Mandatory = $true)]
    [ValidateSet('new-project')]
    $Command,
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]]
    $Args
)

Push-Location $PSScriptRoot

try {
    switch($Command) {
        'new-project' {
            $templ = $Args[0]
            $name = $Args[1]
            dotnet new ft-$templ -o src\$name && `
            dotnet new ft-xunit -o test\Test.$name -tp $name && `
            dotnet sln add src\$name\$($name).csproj && `
            dotnet sln add test\Test.$name\Test.$($name).csproj
        }
    }
}
finally {
    Pop-Location
}

