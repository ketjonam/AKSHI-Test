using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes;

[SetUpFixture]
[NonParallelizable]
public sealed class BiznesAssemblySetup
{
    [OneTimeSetUp]
    [Timeout(300000)]
    public async Task LoginOnceThenRunAllBiznesTests()
    {
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === BIZNES: login nje here me M53330201S, pastaj te gjitha testet Organisation ===");
        await AuthSession.EnsureAsync(LoginProfile.Biznes);
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === BIZNES: sesioni u ruajt, nisin te gjitha testet e biznesit ===");
    }
}
