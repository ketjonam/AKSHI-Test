namespace AKSHI.Test;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
public class TestRunAQTN
{
    public static string Script => """
        run tests.qytetar.aqtn
        skip dergo
        """;

    [Test]
    public void Run() => Core.TestRunHost.Execute(Script);
}
