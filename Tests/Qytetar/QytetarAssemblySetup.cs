using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar;

[SetUpFixture]
[NonParallelizable]
public sealed class QytetarAssemblySetup
{
    [OneTimeSetUp]
    [Timeout(900000)]
    public async Task LoginThenRunQytetarTests()
    {
        IReadOnlyList<LoginProfile> profiles = QytetarLoginPlanner.Resolve();
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === QYTETAR: login per {string.Join(", ", profiles.Select(p => SettingsLoader.AccountFor(p).Username))} ===");

        foreach (LoginProfile profile in profiles)
        {
            string nid = SettingsLoader.AccountFor(profile).Username;
            TestContext.Progress.WriteLine(
                $"{DateTime.Now:HH:mm:ss} | === QYTETAR: login nje here me {nid} ===");
            await AuthSession.EnsureAsync(profile);
        }

        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === QYTETAR: sesionet u ruajten, nisin testet ===");
    }
}
