using Microsoft.Playwright;

namespace AKSHI.Test.Core;

public sealed class PlaywrightCommands
{
    private readonly HashSet<string> _testRun;
    private readonly HashSet<string> _testSkip;
    private readonly HashSet<string> _stepRun;
    private readonly HashSet<string> _stepSkip;

    public PlaywrightCommands(IPage page, PlaywrightUi ui, CommandFilterSettings? settings = null)
    {
        Page = page;
        Ui = ui;
        _testRun = NewSet(settings?.Run);
        _testSkip = NewSet(settings?.Skip);
        _stepRun = NewSet(settings?.StepRun);
        _stepSkip = NewSet(settings?.StepSkip);
    }

    public IPage Page { get; }
    public PlaywrightUi Ui { get; }

    public PlaywrightCommands Run(params string[] names)
    {
        AddRange(_stepRun, names);
        return this;
    }

    public PlaywrightCommands Skip(params string[] names)
    {
        AddRange(_stepSkip, names);
        return this;
    }

    public PlaywrightCommands Only(params string[] names)
    {
        _stepRun.Clear();
        return Run(names);
    }

    public PlaywrightCommands RunTests(params string[] names)
    {
        AddRange(_testRun, names);
        return this;
    }

    public PlaywrightCommands SkipTests(params string[] names)
    {
        AddRange(_testSkip, names);
        return this;
    }

    public PlaywrightCommands OnlyTests(params string[] names)
    {
        _testRun.Clear();
        return RunTests(names);
    }

    public PlaywrightCommands Clear()
    {
        _stepRun.Clear();
        _stepSkip.Clear();
        return this;
    }

    public bool ShouldExecute(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;
        if (IsListed(_stepSkip, name))
            return false;
        if (_stepRun.Count == 0)
            return true;
        return IsListed(_stepRun, name);
    }

    public void IgnoreIfTestSkipped(params string[] names)
    {
        var candidates = names
            .Concat(CurrentTestNames())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (ShouldRunTest(candidates))
            return;

        string reason = candidates.Any(c => IsListed(_testSkip, c))
            ? $"Testi u anashkalua nga Commands.Skip ({string.Join(", ", _testSkip)})."
            : $"Testi nuk eshte ne Commands.Run ({string.Join(", ", _testRun)}).";
        Assert.Ignore(reason);
    }

    public bool ShouldRunTest(IEnumerable<string> names) =>
        CommandScript.ShouldRunTest(names, new CommandFilterSettings
        {
            Run = _testRun.ToList(),
            Skip = _testSkip.ToList()
        });

    public async Task ExecuteAsync(string name, Func<Task> action)
    {
        if (!ShouldExecute(name))
        {
            LogSkip(name);
            return;
        }

        LogRun(name);
        await action();
    }

    public async Task<T?> ExecuteAsync<T>(string name, Func<Task<T>> action)
    {
        if (!ShouldExecute(name))
        {
            LogSkip(name);
            return default;
        }

        LogRun(name);
        return await action();
    }

    public void Execute(string name, Action action)
    {
        if (!ShouldExecute(name))
        {
            LogSkip(name);
            return;
        }

        LogRun(name);
        action();
    }

    public T? Execute<T>(string name, Func<T> action)
    {
        if (!ShouldExecute(name))
        {
            LogSkip(name);
            return default;
        }

        LogRun(name);
        return action();
    }

    public Task GotoAsync(string name, string url) =>
        ExecuteAsync(name, () => Page.GotoAsync(url, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        }));

    public Task ClickAsync(string name, string selector, int timeoutMs = 15000) =>
        ExecuteAsync(name, () => Ui.SafeClickAsync(selector, timeoutMs));

    public Task JsClickAsync(string name, string selector) =>
        ExecuteAsync(name, () => Ui.JsClickAsync(selector));

    public Task FillAsync(string name, string selector, string value) =>
        ExecuteAsync(name, () => Ui.FillAsync(selector, value));

    public Task TypeAsync(string name, string selector, string value) =>
        ExecuteAsync(name, () => Ui.TypeAsync(selector, value));

    public Task ClearInputAsync(string name, string selector) =>
        ExecuteAsync(name, () => Ui.ClearInputAsync(selector));

    public Task SelectByValueAsync(string name, string selector, string value) =>
        ExecuteAsync(name, () => Ui.SelectByValueAsync(selector, value));

    public Task SelectByTextAsync(string name, string selector, string label) =>
        ExecuteAsync(name, () => Ui.SelectByTextAsync(selector, label));

    public Task<ILocator?> WaitVisibleAsync(string name, string selector, int timeoutMs = 15000) =>
        ExecuteAsync(name, () => Ui.WaitVisibleAsync(selector, timeoutMs));

    public Task WaitHiddenAsync(string name, string selector, int timeoutMs = 15000) =>
        ExecuteAsync(name, () => Ui.WaitHiddenAsync(selector, timeoutMs));

    public ILocator Locator(string selector) => Page.Locator(selector);

    private static IEnumerable<string> CurrentTestNames()
    {
        var test = TestContext.CurrentContext.Test;
        yield return test.Name;
        yield return test.MethodName ?? string.Empty;
        yield return test.ClassName ?? string.Empty;
        yield return test.FullName;

        if (!test.Properties.ContainsKey("Category"))
            yield break;

        foreach (object? category in test.Properties["Category"])
        {
            if (category is not null)
                yield return category.ToString() ?? string.Empty;
        }
    }

    private static HashSet<string> NewSet(IEnumerable<string>? values)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        AddRange(set, values);
        return set;
    }

    private static void AddRange(HashSet<string> set, IEnumerable<string>? values)
    {
        if (values is null)
            return;

        foreach (string value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;
            set.Add(value.Trim());
        }
    }

    private static bool IsListed(HashSet<string> set, string name)
    {
        if (set.Count == 0)
            return false;

        foreach (string filter in set)
        {
            if (CommandScript.Matches(name, filter))
                return true;
        }

        return false;
    }

    private static void LogRun(string name) =>
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Playwright command RUN: {name}");

    private static void LogSkip(string name) =>
        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | Playwright command SKIP: {name}");
}
