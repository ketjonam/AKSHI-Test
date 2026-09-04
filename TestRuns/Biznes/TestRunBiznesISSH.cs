namespace AKSHI.Test.TestRuns.Biznes;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Biznes")]
public class TestRunBiznesISSH
{
    public static string Script => """
        run tests.biznes.issh
        skip dergo
        """;

    [Test]
    public void Run_Biznes_ISSH() => Core.TestRunHost.Execute(Script);
}
