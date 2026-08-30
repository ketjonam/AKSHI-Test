$ErrorActionPreference = "Stop"

$SourceRoot = "C:\Users\Kreatx\source\repos\Test-Automation-"
$DestRoot = "C:\Users\Kreatx\source\repos\AKSHI-Test"
$SkipDirs = @("MS Automation refactor", "bin", "obj", ".vs", "TestArtifacts")
$DropMethods = @("Setup","TearDown","Log","SaveScreenshot","SavePageSource","SafeClick")

function Get-BraceBody([string]$text, [int]$start) {
    $depth = 0
    $inStr = $null
    $escape = $false
    for ($i = $start; $i -lt $text.Length; $i++) {
        $ch = $text[$i]
        if ($null -ne $inStr) {
            if ($escape) { $escape = $false }
            elseif ($ch -eq "\") { $escape = $true }
            elseif ($ch -eq $inStr) { $inStr = $null }
        }
        else {
            if ($ch -eq '"' -or $ch -eq "'") { $inStr = $ch }
            elseif ($ch -eq "{") { $depth++ }
            elseif ($ch -eq "}") {
                $depth--
                if ($depth -eq 0) {
                    return @{ Body = $text.Substring($start + 1, $i - $start - 1); End = $i + 1 }
                }
            }
        }
    }
    return @{ Body = $text.Substring($start + 1); End = $text.Length }
}

function Get-HumanTitle([string]$name) {
    $name = $name -replace "(_FailCase|_)$",""
    $name = [regex]::Replace($name, "([a-z])([A-Z])", '$1 $2')
    $name = [regex]::Replace($name, "(\d+)", ' $1 ')
    return ($name -replace "\s+", " ").Trim()
}

function Get-Login([string]$profile) {
    if ($profile -match "^Org") { return "Biznes" }
    return "Qytetar"
}

function Get-Field([string]$body, [string]$field) {
    $m = [regex]::Match($body, ('Id\("' + [regex]::Escape($field) + '"\)\)\.SendKeys\("([^"]*)"\)'))
    if ($m.Success) { return $m.Groups[1].Value }
    return $null
}

function Get-Profile([string]$body) {
    $m = [regex]::Match($body, 'SelectByValue\("(Individual|Organisation)"\)')
    if ($m.Success) { return $m.Groups[1].Value }
    return "Individual"
}

function Get-ServiceCode([string]$body, [string]$stem, [string]$testName) {
    $code = Get-Field $body "ServiceCode"
    if ($code -and $code -match '^\d+$') { return $code }
    $found = [regex]::Matches($stem + " " + $testName, '\d+')
    if ($found.Count -gt 0) { return $found[0].Value }
    return "0"
}

function Test-IsTrack([string]$name, [string]$body) {
    if ($name -match 'Gjurmo') { return $true }
    return [regex]::IsMatch($body, "Click ['\`"]Gjurmo|aria-label=['\`"]Gjurmo", "IgnoreCase")
}

function Get-Ident([string]$value) {
    $value = [regex]::Replace($value, '[^A-Za-z0-9_]', '_')
    if ([string]::IsNullOrWhiteSpace($value)) { $value = "Test" }
    if ($value[0] -match '\d') { $value = "_" + $value }
    return $value
}

function Unwrap-InlineDriver([string]$body) {
    $body = [regex]::Replace($body, '\s*var options = new EdgeOptions\(\);\s*options\.AddArgument\([^;]+;', "`n")
    $usingM = [regex]::Match($body, 'using\s*\(\s*IWebDriver\s+driver\s*=\s*new\s+EdgeDriver\([^)]*\)\s*\)\s*\{')
    if ($usingM.Success) {
        $inner = Get-BraceBody $body ($usingM.Index + $usingM.Length - 1)
        $tryM = [regex]::Match($inner.Body, 'try\s*\{')
        if ($tryM.Success) {
            $tryBody = Get-BraceBody $inner.Body ($tryM.Index + $tryM.Length - 1)
            $body = $body.Substring(0, $usingM.Index) + $tryBody.Body + "`n"
        }
        else {
            $cleaned = [regex]::Replace($inner.Body, '\s*var wait = new WebDriverWait\(driver,\s*TimeSpan\.FromSeconds\(\d+\)\);\s*', "`n")
            $body = $body.Substring(0, $usingM.Index) + $cleaned + "`n"
        }
    }
    else {
        $body = [regex]::Replace($body, '\s*(?:IWebDriver\s+)?driver\s*=\s*new\s+EdgeDriver\([^;]+;', "`n")
        $body = [regex]::Replace($body, '\s*var wait = new WebDriverWait\(driver,\s*TimeSpan\.FromSeconds\(\d+\)\);\s*', "`n")
    }
    $body = [regex]::Replace($body, '\s*string runTime = DateTime\.Now\.ToString\([^;]+;\s*string testName = TestContext\.CurrentContext\.Test\.Name;\s*string artifactsFolder = Path\.Combine\([\s\S]*?\);\s*Directory\.CreateDirectory\(artifactsFolder\);\s*', "`n")
    $body = [regex]::Replace($body, '\s*Log\("===== TEST START ====="\);\s*', "`n")
    $body = [regex]::Replace($body, '\s*Log\("Artifacts folder: " \+ artifactsFolder\);\s*', "`n")
    return $body
}

function Strip-OneBootstrap([string]$body) {
    $markers = @(
        'Log\("Open website"\);',
        'Log\("Open Website"\);',
        'driver\.Navigate\(\)\.GoToUrl',
        'Log\("Kliko Test Sherbimesh"\);',
        "Log\(`"Click 'Test Sherbimesh' button`"\);",
        'Log\("Click service button"\);',
        'FindElement\(By\.Id\("Nid"\)\)',
        'FindElement\(By\.Id\("ServiceCode"\)\)'
    )
    $start = $null
    foreach ($marker in $markers) {
        $m = [regex]::Match($body, $marker)
        if ($m.Success) {
            if ($null -eq $start -or $m.Index -lt $start) { $start = $m.Index }
        }
    }
    if ($null -eq $start) { return $null }

    $rest = $body.Substring($start)
    $patterns = @(
        'SafeClick\(By\.XPath\(aplikimiRiXpath\)\);\s*(?:Thread\.Sleep\(\d+\);)?',
        'wait\.Until\(ExpectedConditions\.ElementToBeClickable\(\s*By\.XPath\("//button\[@aria-label=''Aplikim i ri''\]"\)\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?',
        'driver\.FindElement\(By\.XPath\("//button\[@aria-label=''Aplikim i ri''\]"\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?',
        'wait\.Until\(ExpectedConditions\.ElementToBeClickable\(\s*By\.XPath\([^)]*Gjurmo[^)]*\)\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?',
        'FindElement\(By\.ClassName\("load-button"\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?'
    )
    $bestEnd = $null
    foreach ($pat in $patterns) {
        $m = [regex]::Match($rest, $pat, [System.Text.RegularExpressions.RegexOptions]::Singleline -bor [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
        if ($m.Success) {
            $end = $m.Index + $m.Length
            if ($null -eq $bestEnd -or $end -gt $bestEnd) { $bestEnd = $end }
        }
    }
    if ($null -eq $bestEnd) { return $null }

    $prefix = $body.Substring(0, $start)
    $suffix = $rest.Substring($bestEnd)
    $prefix = [regex]::Replace($prefix, '\s*string (serviceButtonXpath|aplikimiRiXpath)\s*=\s*"[^"]*";\s*', "`n")
    return $prefix + $suffix
}

function Strip-Bootstrap([string]$body) {
    $body = Unwrap-InlineDriver $body
    for ($i = 0; $i -lt 6; $i++) {
        $nxt = Strip-OneBootstrap $body
        if ($null -eq $nxt) { break }
        $body = $nxt
    }
    $body = [regex]::Replace($body, '\s*new SelectElement\(driver\.FindElement\(By\.Id\("Platform"\)\)\)\s*\.SelectByValue\("[^"]+"\);\s*', "`n")
    $body = [regex]::Replace(
        $body,
        '(?s)\r?\n\s*\}\s*catch\s*\(Exception[^\)]*\)\s*\{.*?\}\s*finally\s*\{.*?\}\s*$',
        ''
    )
    $body = [regex]::Replace(
        $body,
        '(?s)\r?\n\s*catch\s*\(Exception[^\)]*\)\s*\{.*?\}\s*finally\s*\{.*?\}\s*$',
        ''
    )
    return $body
}

function Get-Methods([string]$content) {
    $pattern = '(?m)(?<attrs>(?:\[[^\]]+\]\s*)*)(?<sig>(?:public|private|protected)\s+(?:static\s+)?(?:async\s+)?(?<ret>[\w.<>,\[\]\s]+?)\s+(?<name>\w+)\s*\((?<params>[^)]*)\)\s*)\{'
    $methods = @()
    foreach ($m in [regex]::Matches($content, $pattern)) {
        $name = $m.Groups["name"].Value
        $start = $m.Index + $m.Length - 1
        $block = Get-BraceBody $content $start
        $methods += [pscustomobject]@{
            Attrs = $m.Groups["attrs"].Value
            Sig = $m.Groups["sig"].Value.TrimEnd()
            Ret = $m.Groups["ret"].Value.Trim()
            Name = $name
            Params = $m.Groups["params"].Value.Trim()
            Body = $block.Body
            IsTest = $m.Groups["attrs"].Value.Contains("[Test]")
        }
    }
    return $methods
}

function Build-Class($institution, $login, $className, $tests, $helpers, $sourceStem) {
    $first = $tests[0]
    $serviceCode = "0"
    foreach ($t in $tests) {
        $code = Get-ServiceCode $t.Body $sourceStem $t.Name
        if ($code -ne "0") { $serviceCode = $code; break }
    }
    $track = $false
    foreach ($t in $tests) { if (Test-IsTrack $t.Name $t.Body) { $track = $true } }
    $startMode = if ($track) { "ServiceStartMode.Track" } else { "ServiceStartMode.NewApplication" }
    $title = $first.Name
    $base = if ($login -eq "Biznes") { "BiznesTestBase" } else { "QytetarTestBase" }
    $nsInst = Get-Ident $institution
    $ns = "AKSHI.Test.Tests.$login.$nsInst"
    $categories = @("[Category(`"$institution`")]", "[Category(`"$serviceCode`")]")
    $fail = $false
    $mobile = $false
    foreach ($t in $tests) {
        if ($t.Name -match "FailCase" -or $className -match "FailCase") { $fail = $true }
        if ($t.Name -match "Mobile" -or $className -match "Mobile") { $mobile = $true }
    }
    if ($fail) { $categories += '[Category("FailCase")]' }
    if ($mobile) { $categories += '[Category("Mobile")]' }

    $methodSrc = New-Object System.Collections.Generic.List[string]
    $catalogItems = @()
    foreach ($t in $tests) {
        $body = Strip-Bootstrap $t.Body
        $tCode = Get-ServiceCode $t.Body $sourceStem $t.Name
        if ($tCode -eq "0") { $tCode = $serviceCode }
        $tProfile = Get-Profile $t.Body
        $tLogin = Get-Login $tProfile
        $catalogItems += [ordered]@{
            code = $tCode
            title = $t.Name
            institution = $institution
            profileType = $tProfile
            loginProfile = $tLogin
            search = $tCode
            startMode = $(if (Test-IsTrack $t.Name $t.Body) { "Track" } else { "NewApplication" })
            sourceFile = "$institution\$className.cs"
        }
        $testName = Get-Ident $t.Name
        $methodSrc.Add("    [Test]`r`n    public void $testName()`r`n    {`r`n$($body.TrimEnd())`r`n    }`r`n")
    }

    $helperSrc = New-Object System.Collections.Generic.List[string]
    foreach ($h in $helpers) {
        if ($h.Name -eq $className) { continue }
        $helperSrc.Add("    private $($h.Ret) $($h.Name)($($h.Params))`r`n    {`r`n$($h.Body.TrimEnd())`r`n    }`r`n")
    }

    $src = @"
using AKSHI.Test.Core;

namespace $ns;

$($categories -join "`r`n")
public class $className : $base
{
    protected override string ServiceCode => "$serviceCode";
    protected override string? ServiceTitle => "$title";
    protected override ServiceStartMode StartMode => $startMode;

$($methodSrc -join "`r`n")
$($helperSrc -join "`r`n")}
"@
    return @{ Src = $src; Catalog = $catalogItems }
}

$files = Get-ChildItem -Path $SourceRoot -Filter *.cs -Recurse -File | Where-Object {
    $rel = $_.FullName.Substring($SourceRoot.Length).TrimStart('\')
    $parts = $rel.Split('\')
    -not ($parts | Where-Object { $SkipDirs -contains $_ })
}

$catalog = @()
$generated = 0
$skipped = 0

foreach ($file in $files) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $classM = [regex]::Match($content, 'public class (\w+)')
    if (-not $classM.Success) { $skipped++; continue }
    $rel = $file.FullName.Substring($SourceRoot.Length).TrimStart('\')
    $institution = $rel.Split('\')[0]
    $methods = @(Get-Methods $content)
    $tests = @($methods | Where-Object { $_.IsTest })
    if ($tests.Count -eq 0) { $skipped++; continue }
    $helpers = @($methods | Where-Object { -not $_.IsTest -and $DropMethods -notcontains $_.Name })

    $byLogin = @{}
    foreach ($t in $tests) {
        $login = Get-Login (Get-Profile $t.Body)
        if (-not $byLogin.ContainsKey($login)) { $byLogin[$login] = @() }
        $byLogin[$login] += $t
    }

    foreach ($login in $byLogin.Keys) {
        $className = $classM.Groups[1].Value
        if ($byLogin.Keys.Count -gt 1) { $className = "${className}_${login}" }
        $built = Build-Class $institution $login $className $byLogin[$login] $helpers $file.BaseName
        $folder = Join-Path $DestRoot "Tests\$login\$institution"
        New-Item -ItemType Directory -Force -Path $folder | Out-Null
        $out = Join-Path $folder "$className.cs"
        [System.IO.File]::WriteAllText($out, $built.Src, [System.Text.UTF8Encoding]::new($false))
        $catalog += $built.Catalog
        $generated++
        Write-Host "OK Tests\$login\$institution\$className.cs"
    }
}

$unique = @()
$seen = @{}
foreach ($item in $catalog) {
    $key = "$($item.code)|$($item.title)|$($item.loginProfile)"
    if ($seen.ContainsKey($key)) { continue }
    $seen[$key] = $true
    $unique += $item
}

$configDir = Join-Path $DestRoot "Config"
New-Item -ItemType Directory -Force -Path $configDir | Out-Null
$json = $unique | ConvertTo-Json -Depth 6
[System.IO.File]::WriteAllText((Join-Path $configDir "services.json"), $json, [System.Text.UTF8Encoding]::new($false))
Write-Host "Generated $generated test classes, catalog $($unique.Count), skipped files $skipped"
