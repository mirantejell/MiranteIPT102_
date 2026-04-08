    param(
    [string]$ServerInstance = "(localdb)\MSSQLLocalDB",
    [string]$DatabaseName = "MiranteIPT102"
)

Write-Host "Setting up database: $DatabaseName on $ServerInstance" -ForegroundColor Green

$scriptPath = Join-Path $PSScriptRoot "DatabaseSetup.sql"

if (-not (Test-Path $scriptPath)) {
    Write-Host "Error: DatabaseSetup.sql not found at $scriptPath" -ForegroundColor Red
    exit 1
}

try {
    sqlcmd -S $ServerInstance -i $scriptPath
    Write-Host "Database setup completed successfully!" -ForegroundColor Green
}
catch {
    Write-Host "Error setting up database: $_" -ForegroundColor Red
    exit 1
}
