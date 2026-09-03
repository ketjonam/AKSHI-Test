namespace AKSHI.Test;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
public class TestRunAMS
{
    public static string Script => """
        run tests.qytetar.ams
        skip dergo
        """;

    [Test]
    public void Run() => Core.TestRunHost.Execute(Script);
}
