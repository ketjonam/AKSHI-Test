using Microsoft.Playwright;

namespace AKSHI.Test.Core;

public static class Loc
{
    public static string Id(string id) => $"#{id}";
    public static string Css(string css) => css;
    public static string XPath(string xpath) => $"xpath={xpath}";
    public static string Class(string className) => $".{className}";
}

public sealed class PlaywrightUi
{
    private readonly IPage _page;

    public PlaywrightUi(IPage page)
    {
        _page = page;
    }

    public ILocator Locator(string selector) => _page.Locator(selector);

    public async Task SafeClickAsync(string selector, int timeoutMs = 15000)
    {
        ILocator element = _page.Locator(selector).First;
        await element.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Attached,
            Timeout = timeoutMs
        });
        await element.ScrollIntoViewIfNeededAsync();
        try
        {
            await element.ClickAsync(new LocatorClickOptions { Timeout = timeoutMs });
        }
        catch (PlaywrightException)
        {
            await element.ClickAsync(new LocatorClickOptions { Force = true, Timeout = timeoutMs });
        }
    }

    public async Task<ILocator> WaitVisibleAsync(string selector, int timeoutMs = 15000)
    {
        ILocator element = _page.Locator(selector).First;
        await element.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeoutMs
        });
        return element;
    }

    public async Task FillAsync(string selector, string value)
    {
        ILocator element = await WaitVisibleAsync(selector);
        await element.FillAsync(value);
    }

    public async Task TypeAsync(string selector, string value)
    {
        ILocator element = await WaitVisibleAsync(selector);
        await element.ClickAsync();
        await element.PressSequentiallyAsync(value);
    }

    public async Task<string> TextAsync(string selector)
    {
        ILocator element = await WaitVisibleAsync(selector);
        return (await element.InnerTextAsync()).Trim();
    }

    public async Task<string> ValueAsync(string selector)
    {
        ILocator element = await WaitVisibleAsync(selector);
        return (await element.InputValueAsync()).Trim();
    }

    public async Task<string?> AttributeAsync(string selector, string name)
    {
        ILocator element = await WaitVisibleAsync(selector);
        return await element.GetAttributeAsync(name);
    }

    public async Task<int> CountAsync(string selector) =>
        await _page.Locator(selector).CountAsync();

    public async Task<IReadOnlyList<ILocator>> AllAsync(string selector) =>
        await _page.Locator(selector).AllAsync();

    public async Task SelectByValueAsync(string selector, string value)
    {
        ILocator element = await WaitVisibleAsync(selector);
        await element.SelectOptionAsync(new SelectOptionValue { Value = value });
    }

    public async Task SelectByTextAsync(string selector, string label)
    {
        ILocator element = await WaitVisibleAsync(selector);
        await element.SelectOptionAsync(new SelectOptionValue { Label = label });
    }

    public async Task<IReadOnlyList<string>> SelectOptionValuesAsync(string selector)
    {
        ILocator options = _page.Locator($"{selector} option");
        string[] values = await options.EvaluateAllAsync<string[]>("els => els.map(e => e.value || '')")
            ?? Array.Empty<string>();
        return values;
    }

    public async Task<IReadOnlyList<string>> SelectOptionTextsAsync(string selector)
    {
        ILocator options = _page.Locator($"{selector} option");
        return await options.AllInnerTextsAsync();
    }

    public async Task AssertReadonlyAsync(string id, string expectedValue)
    {
        ILocator input = await WaitVisibleAsync(Loc.Id(id));
        Assert.That(await input.GetAttributeAsync("readonly"), Is.Not.Null);
        Assert.That((await input.InputValueAsync()).Trim(), Is.EqualTo(expectedValue));
    }

    public async Task AssertLabelAsync(string forId, string expectedLabel)
    {
        ILocator label = await WaitVisibleAsync($"label[for='{forId}']");
        Assert.That((await label.InnerTextAsync()).Trim(), Is.EqualTo(expectedLabel));
    }

    public async Task WaitHiddenAsync(string selector, int timeoutMs = 15000)
    {
        await _page.Locator(selector).First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = timeoutMs
        });
    }

    public async Task JsClickAsync(string selector)
    {
        await _page.Locator(selector).First.EvaluateAsync("el => el.click()");
    }

    public async Task BlurActiveElementAsync()
    {
        await _page.EvaluateAsync("() => { if (document.activeElement) document.activeElement.blur(); }");
    }

    public async Task ClearInputAsync(string selector)
    {
        ILocator input = await WaitVisibleAsync(selector);
        await input.ClickAsync();
        await input.FillAsync(string.Empty);
        await BlurActiveElementAsync();
    }

    public async Task WaitForRowsAsync(string selector, int timeoutMs = 40000)
    {
        await _page.WaitForFunctionAsync(
            @"sel => {
                const rows = document.querySelectorAll(sel);
                return rows.length > 0 && rows[0].offsetParent !== null;
            }",
            selector,
            new PageWaitForFunctionOptions { Timeout = timeoutMs });
    }
}
