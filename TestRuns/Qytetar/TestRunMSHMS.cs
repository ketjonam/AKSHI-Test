namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunMSHMS
{
    public static string Script => """
        run tests.qytetar.mshms
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_MSHMS() => Core.TestRunHost.Execute(Script);
}
