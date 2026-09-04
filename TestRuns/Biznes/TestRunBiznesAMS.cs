namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznesAMS
{
    public static string Script => """
        run tests.biznes.ams
        skip dergo
        """;

    [Test]
    public void Run_Biznes_AMS() => Core.TestRunHost.Execute(Script);
}
