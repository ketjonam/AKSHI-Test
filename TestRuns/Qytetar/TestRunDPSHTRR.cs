namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunDPSHTRR
{
    public static string Script => """
        run tests.qytetar.dpshtrr
        skip tests.qytetar.dpshtrr-ams
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_DPSHTRR() => Core.TestRunHost.Execute(Script);
}
