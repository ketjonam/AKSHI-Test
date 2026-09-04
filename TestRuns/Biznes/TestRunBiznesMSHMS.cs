namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznesMSHMS
{
    public static string Script => """
        run tests.biznes.mshms
        skip dergo
        """;

    [Test]
    public void Run_Biznes_MSHMS() => Core.TestRunHost.Execute(Script);
}
