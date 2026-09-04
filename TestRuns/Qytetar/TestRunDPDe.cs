namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunDPDe
{
    public static string Script => """
        run tests.qytetar.dpde
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_DPDe() => Core.TestRunHost.Execute(Script);
}
