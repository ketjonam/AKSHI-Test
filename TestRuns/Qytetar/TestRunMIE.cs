namespace AKSHI.Test.TestRuns.Qytetar;

[TestFixture]
[Timeout(900000)]
[NonParallelizable]
[Explicit]
[Category("TestRun")]
[Category("Qytetar")]
public class TestRunMIE
{
    public static string Script => """
        run tests.qytetar.mie
        skip dergo
        """;

    [Test]
    public void Run_Qytetar_MIE() => Core.TestRunHost.Execute(Script);
}
