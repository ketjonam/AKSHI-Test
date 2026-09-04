namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(43200000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunQytetar
{
    public static string Script => """
        run tests.qytetar
        skip 6161
        skip dergo
        """;

    [Test]
    public void Run_Qytetar() => Core.TestRunHost.Execute(Script);
}
