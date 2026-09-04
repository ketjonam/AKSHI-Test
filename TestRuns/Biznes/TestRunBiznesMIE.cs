namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznesMIE
{
    public static string Script => """
        run tests.biznes.mie
        skip 11132
        skip dergo
        """;

    [Test]
    public void Run_Biznes_MIE() => Core.TestRunHost.Execute(Script);
}
