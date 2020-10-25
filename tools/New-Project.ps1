[CmdletBinding()]
param (
    [Parameter(Position = 0, Mandatory = $true)]
    [string]
    $Template,
    [Parameter(Position = 1, Mandatory = $true)]
    [string]
    $Name
)

dotnet new $Template -o "src\$Name"
dotnet sln add "src\$Name\${Name}.csproj"
dotnet new ft-xunit -o "test\Test.$Name" -tp $Name
dotnet sln add "test\Test.$Name\Test.${Name}.csproj"
dotnet add "test\Test.$Name\Test.${Name}.csproj" reference "src\$Name\${Name}.csproj"
