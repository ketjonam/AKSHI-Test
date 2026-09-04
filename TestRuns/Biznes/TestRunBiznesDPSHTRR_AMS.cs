namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznesDPSHTRR_AMS
{
    public static string Script => """
        run tests.biznes.dpshtrr-ams
        skip dergo
        """;

    [Test]
    public void Run_Biznes_DPSHTRR_AMS() => Core.TestRunHost.Execute(Script);
}
