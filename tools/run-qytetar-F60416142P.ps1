$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot\..

$env:AKSHI_QYTETAR_LOGIN = "F60416142P"

Write-Host "AKSHI Test — qytetar F60416142P (ish-NID G35511058E / F60214024S)"
Write-Host "1) Login nje here si qytetar F60416142P"
Write-Host "2) Vendos kodin OTP ne shfletues (deri ne 4 minuta)"
Write-Host "3) Ekzekutohen testet e kesaj llogarie"
Write-Host ""

dotnet test --settings akshi.runsettings --filter "Category=F60416142P"
