namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunMAS
{
    public static string Script => """
        run tests.qytetar.mas
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_MAS() => Core.TestRunHost.Execute(Script);
}
