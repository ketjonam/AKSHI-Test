namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunMEPJ
{
    public static string Script => """
        run tests.qytetar.mepj
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_MEPJ() => Core.TestRunHost.Execute(Script);
}
