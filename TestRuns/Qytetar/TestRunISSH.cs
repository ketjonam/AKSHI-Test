namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(10800000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunISSH
{
    public static string ScriptF60416142P => """
        run 368
        run 381
        run 2304
        run 2306
        run 5031
        run 5034
        run 6162
        run 6163
        run 6164
        run 6165
        run 6166
        run 6167
        run 6169
        run 6171
        run 6172
        run 6175
        run 6176
        run 6177
        run 6178
        run 6179
        run 6180
        run 7171
        run 13019
        skip 6161
        skip dergo
        """;

    public static string ScriptJ35413056V => """
        run 2308
        run 5023
        run 6156
        run 6157
        run 6158
        run 6159
        run 10060
        run 14928
        run 14986
        skip 6161
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_ISSH_F60416142P() => Core.TestRunHost.Execute(ScriptF60416142P);

    [Test]
    public void Run_Qytetar_ISSH_J35413056V() => Core.TestRunHost.Execute(ScriptJ35413056V);
}
