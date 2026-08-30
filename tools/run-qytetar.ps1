$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot\..

$env:AKSHI_QYTETAR_LOGIN = "J35413056V"

Write-Host "AKSHI Test — qytetar J35413056V (ish-NID J55728107R)"
Write-Host "1) Login nje here si qytetar J35413056V"
Write-Host "2) Vendos kodin OTP ne shfletues (deri ne 4 minuta)"
Write-Host "3) Ekzekutohen testet e kesaj llogarie"
Write-Host ""

dotnet test --settings akshi.runsettings --filter "Category=J35413056V"
