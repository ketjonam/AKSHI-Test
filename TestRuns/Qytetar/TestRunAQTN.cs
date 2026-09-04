namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunAQTN
{
    public static string Script => """
        run tests.qytetar.aqtn
        skip dergo
        """;

    [Test]
    public void Run() => Core.TestRunHost.Execute(Script);
}
