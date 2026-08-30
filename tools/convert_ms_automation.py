#!/usr/bin/env python3
"""Convert MS Automation Selenium tests into AKSHI Playwright tests.

Keeps original assertion logic and Selenium-style APIs (driver/wait/By).
Replaces the NID/ServiceCode/ProfileType loader with login as Qytetar or Biznes.
"""
from __future__ import annotations

import json
import re
from collections import defaultdict
from pathlib import Path

SOURCE_ROOT = Path(r"C:\Users\Kreatx\source\repos\Test-Automation-")
DEST_ROOT = Path(r"C:\Users\Kreatx\source\repos\AKSHI-Test")
SKIP_DIR_NAMES = {"MS Automation refactor", "bin", "obj", ".vs", "TestArtifacts"}
DROP_METHODS = {
    "Setup",
    "TearDown",
    "Log",
    "SaveScreenshot",
    "SavePageSource",
    "SafeClick",
}

METHOD_RE = re.compile(
    r"(?P<attrs>(?:\[[^\]]+\]\s*)*)"
    r"(?P<sig>(?:public|private|protected)\s+(?:static\s+)?(?:async\s+)?"
    r"(?P<ret>[\w.<>,\[\]\s]+?)\s+(?P<name>\w+)\s*\((?P<params>[^)]*)\)\s*)"
    r"\{",
    re.M,
)


def humanize(name: str) -> str:
    name = re.sub(r"(_FailCase|_)$", "", name)
    name = re.sub(r"([a-z])([A-Z])", r"\1 \2", name)
    name = re.sub(r"(\d+)", r" \1 ", name)
    return re.sub(r"\s+", " ", name).strip()


def profile_to_login(profile: str) -> str:
    return "Biznes" if str(profile).lower().startswith("org") else "Qytetar"


def is_track_test(name: str, body: str) -> bool:
    if re.search(r"Gjurmo", name, re.I):
        return True
    return bool(re.search(r"Click ['\"]Gjurmo|aria-label=['\"]Gjurmo", body, re.I))


def extract_brace_block(text: str, start: int) -> tuple[str, int]:
    depth = 0
    i = start
    in_str = None
    escape = False
    while i < len(text):
        ch = text[i]
        if in_str:
            if escape:
                escape = False
            elif ch == "\\":
                escape = True
            elif ch == in_str:
                in_str = None
        else:
            if ch in ('"', "'"):
                in_str = ch
            elif ch == "{":
                depth += 1
            elif ch == "}":
                depth -= 1
                if depth == 0:
                    return text[start + 1 : i], i + 1
        i += 1
    return text[start + 1 :], len(text)


def extract_field(body: str, field: str) -> str | None:
    match = re.search(rf'Id\("{field}"\)\)\.SendKeys\("([^"]*)"\)', body)
    return match.group(1) if match else None


def extract_profile(body: str) -> str:
    match = re.search(r'SelectByValue\("(Individual|Organisation)"\)', body)
    return match.group(1) if match else "Individual"


def extract_service_code(body: str, path: Path, test_name: str) -> str:
    code = extract_field(body, "ServiceCode")
    if code and re.fullmatch(r"\d+", code):
        return code
    found = re.findall(r"\d+", path.stem + " " + test_name)
    return found[0] if found else "0"


def find_cs_files() -> list[Path]:
    files = []
    for path in SOURCE_ROOT.rglob("*.cs"):
        if any(part in SKIP_DIR_NAMES for part in path.parts):
            continue
        files.append(path)
    return sorted(files)


def extract_methods(content: str) -> list[dict]:
    methods = []
    for match in METHOD_RE.finditer(content):
        name = match.group("name")
        start = match.end() - 1
        body, _ = extract_brace_block(content, start)
        methods.append(
            {
                "attrs": match.group("attrs") or "",
                "sig": match.group("sig").rstrip(),
                "ret": match.group("ret").strip(),
                "name": name,
                "params": match.group("params").strip(),
                "body": body,
                "is_test": "[Test]" in (match.group("attrs") or ""),
            }
        )
    return methods


def unwrap_inline_driver(body: str) -> str:
    body = re.sub(
        r"\s*var options = new EdgeOptions\(\);\s*options\.AddArgument\([^;]+;",
        "\n",
        body,
    )
    using_m = re.search(r"using\s*\(\s*IWebDriver\s+driver\s*=\s*new\s+EdgeDriver\([^)]*\)\s*\)\s*\{", body)
    if using_m:
        inner, _ = extract_brace_block(body, using_m.end() - 1)
        try_m = re.search(r"try\s*\{", inner)
        if try_m:
            try_body, try_end = extract_brace_block(inner, try_m.end() - 1)
            body = body[: using_m.start()] + try_body + "\n"
        else:
            inner = re.sub(
                r"\s*var wait = new WebDriverWait\(driver,\s*TimeSpan\.FromSeconds\(\d+\)\);\s*",
                "\n",
                inner,
            )
            body = body[: using_m.start()] + inner + "\n"
    else:
        body = re.sub(
            r"\s*(?:IWebDriver\s+)?driver\s*=\s*new\s+EdgeDriver\([^;]+;",
            "\n",
            body,
        )
        body = re.sub(
            r"\s*var wait = new WebDriverWait\(driver,\s*TimeSpan\.FromSeconds\(\d+\)\);\s*",
            "\n",
            body,
        )

    body = re.sub(
        r"\s*string runTime = DateTime\.Now\.ToString\([^;]+;\s*"
        r"string testName = TestContext\.CurrentContext\.Test\.Name;\s*"
        r"string artifactsFolder = Path\.Combine\([\s\S]*?\);\s*"
        r"Directory\.CreateDirectory\(artifactsFolder\);\s*",
        "\n",
        body,
    )
    body = re.sub(r'\s*Log\("===== TEST START ====="\);\s*', "\n", body)
    body = re.sub(r'\s*Log\("Artifacts folder: " \+ artifactsFolder\);\s*', "\n", body)
    return body


def strip_one_bootstrap(body: str) -> str | None:
    markers = [
        r'Log\("Open website"\);',
        r'Log\("Open Website"\);',
        r"driver\.Navigate\(\)\.GoToUrl",
        r'Log\("Kliko Test Sherbimesh"\);',
        r"Log\(\"Click 'Test Sherbimesh' button\"\);",
        r'Log\("Click service button"\);',
        r'FindElement\(By\.Id\("Nid"\)\)',
        r'FindElement\(By\.Id\("ServiceCode"\)\)',
    ]
    start = None
    for marker in markers:
        match = re.search(marker, body)
        if match:
            start = match.start() if start is None else min(start, match.start())
    if start is None:
        return None

    rest = body[start:]
    patterns = [
        r"SafeClick\(By\.XPath\(aplikimiRiXpath\)\);\s*(?:Thread\.Sleep\(\d+\);)?",
        r"wait\.Until\(ExpectedConditions\.ElementToBeClickable\(\s*By\.XPath\(\"//button\[@aria-label='Aplikim i ri'\]\"\)\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?",
        r"driver\.FindElement\(By\.XPath\(\"//button\[@aria-label='Aplikim i ri'\]\"\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?",
        r"wait\.Until\(ExpectedConditions\.ElementToBeClickable\(\s*By\.XPath\([^)]*Gjurmo[^)]*\)\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?",
        r'FindElement\(By\.ClassName\("load-button"\)\)\.Click\(\);\s*(?:Thread\.Sleep\(\d+\);)?',
    ]
    best_end = None
    for pat in patterns:
        match = re.search(pat, rest, re.S | re.I)
        if match:
            end = match.end()
            if best_end is None or end > best_end:
                best_end = end
    if best_end is None:
        return None

    prefix = body[:start]
    suffix = rest[best_end:]
    prefix = re.sub(
        r'\s*string (serviceButtonXpath|aplikimiRiXpath)\s*=\s*"[^"]*";\s*',
        "\n",
        prefix,
    )
    return prefix + suffix


def strip_bootstrap(body: str) -> str:
    body = unwrap_inline_driver(body)
    for _ in range(6):
        nxt = strip_one_bootstrap(body)
        if nxt is None:
            break
        body = nxt
    body = re.sub(
        r"\s*new SelectElement\(driver\.FindElement\(By\.Id\(\"Platform\"\)\)\)\s*\.SelectByValue\(\"[^\"]+\"\);\s*",
        "\n",
        body,
    )
    return body


def indent_block(text: str, spaces: int = 8) -> str:
    pad = " " * spaces
    lines = text.replace("\r\n", "\n").split("\n")
    out = []
    for line in lines:
        stripped = line.strip("\n")
        if stripped.strip() == "":
            out.append("")
        else:
            out.append(pad + stripped.lstrip() if stripped.startswith("        ") is False and stripped[:1].strip() else stripped)
    # keep original relative indent if already indented
    if any(line.startswith("        ") for line in lines if line.strip()):
        return "\n".join(lines).rstrip() + "\n"
    return "\n".join(out).rstrip() + "\n"


def sanitize_ident(value: str) -> str:
    value = re.sub(r"[^A-Za-z0-9_]", "_", value)
    if not value:
        value = "Test"
    if value[0].isdigit():
        value = "_" + value
    return value


def parse_file(path: Path) -> dict | None:
    content = path.read_text(encoding="utf-8", errors="ignore")
    class_m = re.search(r"public class (\w+)", content)
    if not class_m:
        return None
    institution = path.relative_to(SOURCE_ROOT).parts[0]
    methods = extract_methods(content)
    tests = [m for m in methods if m["is_test"]]
    if not tests:
        return None
    helpers = [m for m in methods if not m["is_test"] and m["name"] not in DROP_METHODS]
    return {
        "path": path,
        "institution": institution,
        "class_name": class_m.group(1),
        "tests": tests,
        "helpers": helpers,
    }


def build_class(institution: str, login: str, class_name: str, tests: list[dict], helpers: list[dict]) -> tuple[str, list[dict]]:
    first = tests[0]
    service_code = extract_service_code(first["body"], Path("x"), first["name"])
    # prefer service code from any test that has one
    for t in tests:
        code = extract_service_code(t["body"], Path("x"), t["name"])
        if code != "0":
            service_code = code
            break
    track = any(is_track_test(t["name"], t["body"]) for t in tests)
    start_mode = "ServiceStartMode.Track" if track else "ServiceStartMode.NewApplication"
    title = tests[0]["name"]
    base = "BiznesTestBase" if login == "Biznes" else "QytetarTestBase"
    ns = f"AKSHI.Test.Tests.{login}.{sanitize_ident(institution)}"
    categories = [f'[Category("{institution}")]', f'[Category("{service_code}")]']
    if any("FailCase" in t["name"] or "FailCase" in class_name for t in tests):
        categories.append('[Category("FailCase")]')
    if any("Mobile" in t["name"] or "Mobile" in class_name for t in tests):
        categories.append('[Category("Mobile")]')

    method_src = []
    catalog_items = []
    for t in tests:
        body = strip_bootstrap(t["body"])
        t_code = extract_service_code(t["body"], Path("x"), t["name"]) or service_code
        t_profile = extract_profile(t["body"])
        catalog_items.append(
            {
                "code": t_code,
                "title": t["name"],
                "institution": institution,
                "profileType": t_profile,
                "loginProfile": login,
                "search": t_code,
                "startMode": "Track" if is_track_test(t["name"], t["body"]) else "NewApplication",
                "sourceFile": str(Path(institution) / Path(class_name + ".cs")),
            }
        )
        method_src.append(
            f"    [Test]\n    public void {sanitize_ident(t['name'])}()\n    {{\n{indent_block(body, 8)}    }}\n"
        )

    helper_src = []
    for h in helpers:
        # skip constructors and property-like leftovers
        if h["name"] == class_name:
            continue
        helper_src.append(
            f"    private {h['ret']} {h['name']}({h['params']})\n    {{\n{indent_block(h['body'], 8)}    }}\n"
        )

    src = f"""using AKSHI.Test.Core;

namespace {ns};

{chr(10).join(categories)}
public class {class_name} : {base}
{{
    protected override string ServiceCode => "{service_code}";
    protected override string? ServiceTitle => "{title}";
    protected override ServiceStartMode StartMode => {start_mode};

{chr(10).join(method_src)}
{chr(10).join(helper_src)}}}
"""
    return src, catalog_items


def main() -> None:
    grouped: dict[tuple[str, str, str], dict] = {}
    catalog: list[dict] = []
    generated = 0
    skipped = 0

    for path in find_cs_files():
        parsed = parse_file(path)
        if not parsed:
            skipped += 1
            continue

        by_login: dict[str, list[dict]] = defaultdict(list)
        for test in parsed["tests"]:
            login = profile_to_login(extract_profile(test["body"]))
            by_login[login].append(test)

        for login, tests in by_login.items():
            class_name = parsed["class_name"]
            if len(by_login) > 1:
                class_name = f"{class_name}_{login}"
            src, items = build_class(parsed["institution"], login, class_name, tests, parsed["helpers"])
            folder = DEST_ROOT / "Tests" / login / parsed["institution"]
            folder.mkdir(parents=True, exist_ok=True)
            out = folder / f"{class_name}.cs"
            if out.exists() and out.read_text(encoding="utf-8", errors="ignore") != src:
                out = folder / f"{class_name}_{generated}.cs"
            out.write_text(src, encoding="utf-8")
            catalog.extend(items)
            generated += 1
            print(f"OK {out.relative_to(DEST_ROOT)}")

    # unique catalog by code+title+login
    unique = []
    seen = set()
    for item in catalog:
        key = (item["code"], item["title"], item["loginProfile"])
        if key in seen:
            continue
        seen.add(key)
        unique.append(item)

    config_dir = DEST_ROOT / "Config"
    config_dir.mkdir(exist_ok=True)
    (config_dir / "services.json").write_text(
        json.dumps(unique, indent=2, ensure_ascii=False), encoding="utf-8"
    )
    print(f"Generated {generated} test classes, catalog {len(unique)}, skipped files {skipped}")


if __name__ == "__main__":
    main()
