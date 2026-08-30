$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot\..

Write-Host "AKSHI Test — te gjitha testet BIZNES"
Write-Host "1) Login nje here si biznes (M53330201S)"
Write-Host "2) Vendos kodin OTP ne shfletues (deri ne 4 minuta)"
Write-Host "3) Ekzekutohen te gjitha testet me Category=Biznes"
Write-Host ""

dotnet test --settings akshi.runsettings --filter "Category=Biznes"
