$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot\..

$env:AKSHI_QYTETAR_LOGIN = "J70903019W"

Write-Host "AKSHI Test — qytetar J70903019W (ish-NID J25730113W)"
Write-Host "1) Login nje here si qytetar J70903019W"
Write-Host "2) Vendos kodin OTP ne shfletues (deri ne 4 minuta)"
Write-Host "3) Ekzekutohen testet e kesaj llogarie"
Write-Host ""

dotnet test --settings akshi.runsettings --filter "Category=J70903019W"
