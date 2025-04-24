param(
    [Parameter(Mandatory = $true)]
    [string]$Name
)

$command = "dotnet ef migrations add $Name"
$command += " --project Jobs.Infrastructure"
$command += " --startup-project Jobs.API"
$command += " --context ApplicationDbContext"
$command += " --output-dir Data/Migrations"

Write-Host "Executing: $command" -ForegroundColor Cyan
Invoke-Expression $command
