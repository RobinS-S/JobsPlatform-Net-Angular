param(
    [Parameter(Mandatory = $false)]
    [switch]$Force
)

$command = "dotnet ef migrations remove"
$command += " --project Jobs.Infrastructure"
$command += " --startup-project Jobs.API"
$command += " --context ApplicationDbContext"

if ($Force) {
    $command += " --force"
}

Write-Host "Executing: $command" -ForegroundColor Cyan
Invoke-Expression $command
