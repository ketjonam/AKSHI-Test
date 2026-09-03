using System.Diagnostics;
using System.Text;

namespace AKSHI.Test.Core;

public static class TestRunHost
{
    public static void Execute(string script)
    {
        string? filter = CommandScript.ToNUnitFilter(script);
        Assert.That(filter, Is.Not.Null.And.Not.Empty,
            "Shto te pakten nje rresht 'run ...' ne TestRun.Script (p.sh. run tests.qytetar.aqtn).");

        string root = FindProjectRoot();
        string csproj = Path.Combine(root, "AKSHI.Test.csproj");
        Assert.That(File.Exists(csproj), Is.True, "Nuk u gjet AKSHI.Test.csproj ne " + root);

        var psi = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = root,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        psi.ArgumentList.Add("test");
        psi.ArgumentList.Add(csproj);

        string runsettings = Path.Combine(root, "akshi.runsettings");
        if (File.Exists(runsettings))
        {
            psi.ArgumentList.Add("--settings");
            psi.ArgumentList.Add(runsettings);
        }

        psi.ArgumentList.Add("--filter");
        psi.ArgumentList.Add(filter!);
        psi.ArgumentList.Add("--no-build");
        psi.ArgumentList.Add("--nologo");
        psi.Environment["HEADED"] = "1";
        psi.Environment["AKSHI_TESTRUN_SCRIPT"] = script.Replace("\r\n", "\n").Replace('\n', '\u001f');

        TestContext.Progress.WriteLine($"{DateTime.Now:HH:mm:ss} | TestRun: {string.Join(" ", psi.ArgumentList)}");

        using var process = Process.Start(psi);
        Assert.That(process, Is.Not.Null, "Nuk u nis dotnet test.");

        Task<string> outputTask = process!.StandardOutput.ReadToEndAsync();
        Task<string> errorTask = process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        string output = outputTask.GetAwaiter().GetResult();
        string error = errorTask.GetAwaiter().GetResult();

        if (!string.IsNullOrWhiteSpace(output))
            TestContext.Progress.WriteLine(output);
        if (!string.IsNullOrWhiteSpace(error))
            TestContext.Progress.WriteLine(error);

        bool noMatch = output.Contains("No test matches", StringComparison.OrdinalIgnoreCase)
            || error.Contains("No test matches", StringComparison.OrdinalIgnoreCase);
        Assert.That(noMatch, Is.False,
            "Nuk u gjet asnje test per komanden run. Filter: " + filter + Environment.NewLine + output);

        Assert.That(process.ExitCode, Is.EqualTo(0),
            "Testet e TestRun deshtuan." + Environment.NewLine + output + Environment.NewLine + error);
    }

    private static string FindProjectRoot()
    {
        string? dir = AppContext.BaseDirectory;
        while (!string.IsNullOrEmpty(dir))
        {
            if (File.Exists(Path.Combine(dir, "AKSHI.Test.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }

        return AppContext.BaseDirectory;
    }
}
