namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunAMS
{
    public static string Script => """
        run tests.qytetar.ams
        skip dergo
        """;

    [Test]
    public void Run() => Core.TestRunHost.Execute(Script);
}
