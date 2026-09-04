namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(10800000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznes
{
    public static string Script => """
        run tests.biznes
        skip 11132
        skip dergo
        """;

    [Test]
    public void Run_Biznes() => Core.TestRunHost.Execute(Script);
}
