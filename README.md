# AKSHI Test

Automatizim i shërbimeve e-Albania me **Playwright + NUnit**.

Ndryshimi kryesor nga MS Automation (Selenium): shërbimi **nuk** hapet më me formularin NID / ServiceCode / ProfileType.

Rrjedha e ekzekutimit:

1. Login një herë si **qytetar** ose **biznes**
2. Vendos kodin OTP (deri në 4 minuta)
3. Çdo test hap drejtpërdrejt `https://e-albania-test.com/ServiceDetails/{servicecode}` (kodi i atij testi)
4. Niset testi
5. Kur mbaron, testi i radhës hap të njëjtin lloj linku me kodin e vet

Login si qytetar në `https://e-albania-test.com/`: **Hyr** → tab **Qytetar** → NID → fjalëkalim → **VAZHDONI ME IDENTIFIKIMIN** → ti vendos kodin OTP (deri në 4 minuta).

Login si biznes: **Hyr** → tab **Biznes** (`#business-tab`) → NIPT (`#username`) → fjalëkalim (`#password`) → **VAZHDONI ME IDENTIFIKIMIN** → OTP (deri në 4 minuta). Kredencialet e biznesit vendosen te `appsettings.Local.json`.

## Si ekzekutohen testet

| NID origjinal              | Login tani      | Filter NUnit           |
|----------------------------|-----------------|------------------------|
| J55728107R                 | J35413056V      | `Category=J35413056V`  |
| J25730113W                 | J70903019W      | `Category=J70903019W`  |
| G35511058E / F60214024S    | F60416142P      | `Category=F60416142P`  |
| Organisation               | Biznes          | `Category=Biznes`      |

- `dotnet test --filter "Category=Qytetar"` — login për të tre llogaritë e qytetarit (3 OTP), pastaj të gjitha testet Individual
- `dotnet test --filter "Category=J35413056V"` — testet e ish-NID `J55728107R`
- `dotnet test --filter "Category=J70903019W"` — testet e ish-NID `J25730113W`
- `dotnet test --filter "Category=F60416142P"` — testet e ish-NID `G35511058E` dhe `F60214024S`
- `dotnet test --filter "Category=Biznes"` — login një herë si biznes, pastaj të gjitha testet Organisation
- `dotnet test --filter "Category=ISSH"` — vetëm institucionin ISSH

## Konfigurimi

1. Kopjo `appsettings.Local.json.example` si `appsettings.Local.json`
2. Vendos kredencialet e qytetarit (3 llogari) dhe biznesit
3. Nëse portali i testimit nuk është prod, ndrysho `Portal:BaseUrl`

Ose variabla mjedisi:

- `AKSHI_QYTETAR_USERNAME` / `AKSHI_QYTETAR_PASSWORD` (llogaria J35413056V)
- `AKSHI_BIZNES_USERNAME` / `AKSHI_BIZNES_PASSWORD`
- `AKSHI_QYTETAR_LOGIN` — cila llogari qytetari bën login (`J35413056V`, `J70903019W`, `F60416142P`)

## Instalimi

```powershell
dotnet restore
dotnet build
pwsh bin\Debug\net8.0\playwright.ps1 install chromium
```

## Ekzekutimi

```powershell
dotnet test --settings akshi.runsettings --filter "Category=J35413056V"
dotnet test --settings akshi.runsettings --filter "Category=J70903019W"
dotnet test --settings akshi.runsettings --filter "Category=F60416142P"
dotnet test --settings akshi.runsettings --filter "Category=Biznes"
```

Skriptet (login një herë, pastaj testet e asaj llogarie):

```powershell
powershell -File tools\run-qytetar.ps1
powershell -File tools\run-qytetar-J70903019W.ps1
powershell -File tools\run-qytetar-F60416142P.ps1
powershell -File tools\run-biznes.ps1
```

Pa kredenciale, testet **dështojnë** (nuk injorohen).
