using Microsoft.Playwright;

namespace AKSHI.Test.Compat;

internal static class PwSync
{
    public static T Run<T>(Func<Task<T>> operation) =>
        operation().ConfigureAwait(false).GetAwaiter().GetResult();

    public static void Run(Func<Task> operation) =>
        operation().ConfigureAwait(false).GetAwaiter().GetResult();
}

public sealed class By
{
    public string Selector { get; }

    private By(string selector) => Selector = selector;

    public static By Id(string id) => new($"#{id}");
    public static By CssSelector(string css) => new(css);
    public static By XPath(string xpath) => new($"xpath={xpath}");
    public static By ClassName(string className) => new($".{className}");
    public static By TagName(string tagName) => new(tagName);
    public static By Name(string name) => new($"[name='{name}']");
    public static By LinkText(string text) => new($"text={text}");
    public static By PartialLinkText(string text) => new($"text={text}");

    public override string ToString() => Selector;
}

public class WebDriverException : Exception
{
    public WebDriverException(string message) : base(message) { }
    public WebDriverException() : base() { }
}

public class WebDriverTimeoutException : WebDriverException
{
    public WebDriverTimeoutException(string message) : base(message) { }
}

public interface ISearchContext
{
    WebEl FindElement(By by);
    List<WebEl> FindElements(By by);
}

public class NoSuchElementException : WebDriverException
{
    public NoSuchElementException(string message) : base(message) { }
    public NoSuchElementException() : base("Element not found") { }
}

public class StaleElementReferenceException : WebDriverException
{
    public StaleElementReferenceException(string message) : base(message) { }
    public StaleElementReferenceException() : base("Stale element") { }
}

public class ElementClickInterceptedException : WebDriverException
{
    public ElementClickInterceptedException(string message) : base(message) { }
    public ElementClickInterceptedException() : base("Click intercepted") { }
}

public interface ITakesScreenshot
{
    Screenshot GetScreenshot();
}

public sealed class Screenshot
{
    private readonly byte[] _bytes;
    public Screenshot(byte[] bytes) => _bytes = bytes;
    public void SaveAsFile(string path) => File.WriteAllBytes(path, _bytes);
}

public interface IJavaScriptExecutor
{
    object? ExecuteScript(string script, params object[] args);
}

public static class Keys
{
    public const string Control = "Control";
    public const string Delete = "Delete";
    public const string Enter = "Enter";
    public const string Tab = "Tab";
}

public sealed class WebEl : ISearchContext
{
    private static readonly HashSet<string> BooleanAttributeNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "readonly", "disabled", "checked", "selected", "required", "multiple", "hidden"
    };

    internal ILocator Locator { get; }
    private readonly IPage _page;

    internal WebEl(IPage page, ILocator locator)
    {
        _page = page;
        Locator = locator;
    }

    public string Text => (PwSync.Run(() => Locator.InnerTextAsync()) ?? string.Empty);

    public bool Displayed => PwSync.Run(() => Locator.IsVisibleAsync());
    public bool Enabled => PwSync.Run(() => Locator.IsEnabledAsync());
    public bool Selected
    {
        get
        {
            try
            {
                return PwSync.Run(() => Locator.IsCheckedAsync());
            }
            catch (PlaywrightException)
            {
                return PwSync.Run(() => Locator.GetAttributeAsync("selected")) != null;
            }
        }
    }

    public string? GetAttribute(string name)
    {
        if (string.Equals(name, "value", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                return PwSync.Run(() => Locator.InputValueAsync()) ?? string.Empty;
            }
            catch (PlaywrightException)
            {
                return PwSync.Run(() => Locator.GetAttributeAsync("value")) ?? string.Empty;
            }
        }

        string? value = PwSync.Run(() => Locator.GetAttributeAsync(name));
        if (value == null)
            return null;
        if (BooleanAttributeNames.Contains(name) && value.Length == 0)
            return "true";
        return value;
    }

    public string GetDomProperty(string name) => GetAttribute(name) ?? string.Empty;

    public void Click()
    {
        try
        {
            PwSync.Run(() => Locator.ClickAsync());
        }
        catch (PlaywrightException ex)
        {
            throw new ElementClickInterceptedException(ex.Message);
        }
    }

    public void SendKeys(string keys)
    {
        if (keys.Contains(Keys.Control, StringComparison.Ordinal) && keys.Contains('a'))
        {
            PwSync.Run(() => Locator.PressAsync("Control+A"));
            return;
        }

        if (keys == Keys.Delete)
        {
            PwSync.Run(() => Locator.PressAsync("Delete"));
            return;
        }

        if (keys == Keys.Enter)
        {
            PwSync.Run(() => Locator.PressAsync("Enter"));
            return;
        }

        string? inputType = PwSync.Run(() => Locator.GetAttributeAsync("type"));
        if (string.Equals(inputType, "file", StringComparison.OrdinalIgnoreCase))
        {
            PwSync.Run(() => Locator.SetInputFilesAsync(keys));
            return;
        }

        PwSync.Run(async () =>
        {
            await Locator.ClickAsync();
            await Locator.PressSequentiallyAsync(keys);
        });
    }

    public string TagName =>
        (PwSync.Run(() => Locator.EvaluateAsync<string>("el => el.tagName")) ?? string.Empty)
            .ToLowerInvariant();

    public ISearchContext GetShadowRoot() => this;

    public void Clear() => PwSync.Run(() => Locator.FillAsync(string.Empty));

    public WebEl FindElement(By by)
    {
        ILocator child = Locator.Locator(by.Selector);
        if (PwSync.Run(() => child.CountAsync()) == 0)
            throw new NoSuchElementException($"Element not found: {by.Selector}");
        return new WebEl(_page, child.First);
    }

    public List<WebEl> FindElements(By by)
    {
        ILocator child = Locator.Locator(by.Selector);
        int count = PwSync.Run(() => child.CountAsync());
        var list = new List<WebEl>(count);
        for (int i = 0; i < count; i++)
            list.Add(new WebEl(_page, child.Nth(i)));
        return list;
    }
}

public sealed class Navigation
{
    private readonly PwDriver _driver;
    public Navigation(PwDriver driver) => _driver = driver;
    public void GoToUrl(string url) => _driver.NavigateTo(url);
}

public sealed class PwDriver : IJavaScriptExecutor, ITakesScreenshot, ISearchContext
{
    public IPage Page { get; }

    public PwDriver(IPage page) => Page = page;

    public string PageSource => PwSync.Run(() => Page.ContentAsync());
    public Navigation Navigate() => new(this);

    public void Quit() { }
    public void Dispose() { }

    public void NavigateTo(string url) =>
        PwSync.Run(() => Page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded }));

    public WebEl FindElement(By by)
    {
        ILocator locator = Page.Locator(by.Selector);
        if (PwSync.Run(() => locator.CountAsync()) == 0)
            throw new NoSuchElementException($"Element not found: {by.Selector}");
        return new WebEl(Page, locator.First);
    }

    public List<WebEl> FindElements(By by)
    {
        ILocator locator = Page.Locator(by.Selector);
        int count = PwSync.Run(() => locator.CountAsync());
        var list = new List<WebEl>(count);
        for (int i = 0; i < count; i++)
            list.Add(new WebEl(Page, locator.Nth(i)));
        return list;
    }

    public object? ExecuteScript(string script, params object[] args)
    {
        if (args.Length == 0)
            return PwSync.Run(() => Page.EvaluateAsync<object>("(() => { " + script + " })()"));

        if (args.Length == 1 && args[0] is WebEl el)
        {
            return PwSync.Run(() => el.Locator.EvaluateAsync<object>(
                "(element) => { const arguments = [element]; " + script + " }"));
        }

        if (args[0] is WebEl first)
        {
            object extra = args.Length == 2 ? args[1]! : args.Skip(1).ToArray();
            return PwSync.Run(() => first.Locator.EvaluateAsync<object>(
                "(element, extra) => { const arguments = Array.isArray(extra) ? [element, ...extra] : [element, extra]; " + script + " }",
                extra));
        }

        object payload = args.Length == 1 ? args[0]! : args;
        return PwSync.Run(() => Page.EvaluateAsync<object>(
            "(args) => { const arguments = Array.isArray(args) ? args : [args]; " + script + " }",
            payload));
    }

    public Screenshot GetScreenshot()
    {
        byte[] bytes = PwSync.Run(() => Page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true }));
        return new Screenshot(bytes);
    }
}

public sealed class WebDriverWait
{
    private readonly PwDriver _driver;
    private readonly TimeSpan _timeout;

    public WebDriverWait(PwDriver driver, TimeSpan timeout)
    {
        _driver = driver;
        _timeout = timeout;
    }

    public T Until<T>(Func<PwDriver, T> condition)
    {
        DateTime deadline = DateTime.UtcNow + _timeout;
        Exception? last = null;
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                T result = condition(_driver);
                if (result is bool flag)
                {
                    if (flag)
                        return result;
                }
                else if (result is not null)
                {
                    return result;
                }
            }
            catch (Exception ex) when (ex is NoSuchElementException or StaleElementReferenceException or PlaywrightException or WebDriverException)
            {
                last = ex;
            }

            Thread.Sleep(200);
        }

        throw last ?? new WebDriverTimeoutException($"Timed out after {_timeout.TotalSeconds} seconds.");
    }
}

public static class ExpectedConditions
{
    public static Func<PwDriver, WebEl> ElementIsVisible(By by) => driver =>
    {
        WebEl el = driver.FindElement(by);
        if (!el.Displayed)
            throw new NoSuchElementException($"Not visible: {by}");
        return el;
    };

    public static Func<PwDriver, WebEl> ElementToBeClickable(By by) => driver =>
    {
        WebEl el = driver.FindElement(by);
        if (!el.Displayed || !el.Enabled)
            throw new NoSuchElementException($"Not clickable: {by}");
        return el;
    };

    public static Func<PwDriver, WebEl> ElementExists(By by) => driver => driver.FindElement(by);

    public static Func<PwDriver, WebEl> ElementIsVisible(WebEl el) => _ =>
    {
        if (!el.Displayed)
            throw new NoSuchElementException("Not visible");
        return el;
    };

    public static Func<PwDriver, WebEl> ElementToBeClickable(WebEl el) => _ =>
    {
        if (!el.Displayed || !el.Enabled)
            throw new NoSuchElementException("Not clickable");
        return el;
    };

    public static Func<PwDriver, WebEl> ElementExists(WebEl el) => _ => el;

    public static Func<PwDriver, bool> InvisibilityOfElementLocated(By by) => driver =>
    {
        List<WebEl> found = driver.FindElements(by);
        return found.Count == 0 || !found[0].Displayed;
    };
}

public sealed class SelectElement
{
    private readonly WebEl _element;

    public SelectElement(WebEl element) => _element = element;

    public List<WebEl> Options => _element.FindElements(By.CssSelector("option"));

    public WebEl SelectedOption
    {
        get
        {
            List<WebEl> options = Options;
            if (options.Count == 0)
                throw new NoSuchElementException("Select has no options.");

            // React/controlled selects often update select.value without a selected attribute.
            string current = _element.GetAttribute("value") ?? string.Empty;
            WebEl? byValue = options.FirstOrDefault(o =>
                string.Equals(o.GetAttribute("value") ?? string.Empty, current, StringComparison.Ordinal));
            if (byValue is not null)
                return byValue;

            return options.FirstOrDefault(o => o.Selected) ?? options.First();
        }
    }

    public void SelectByValue(string value) =>
        PwSync.Run(() => _element.Locator.SelectOptionAsync(new SelectOptionValue { Value = value }));

    public void SelectByText(string text) =>
        PwSync.Run(() => _element.Locator.SelectOptionAsync(new SelectOptionValue { Label = text }));

    public void SelectByIndex(int index) =>
        PwSync.Run(() => _element.Locator.SelectOptionAsync(new SelectOptionValue { Index = index }));
}
