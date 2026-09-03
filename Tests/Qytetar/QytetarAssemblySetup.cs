using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Qytetar;

[SetUpFixture]
[NonParallelizable]
public sealed class QytetarAssemblySetup
{
    [OneTimeSetUp]
    public void PrepareQytetarAuth()
    {
        Directory.CreateDirectory(SettingsLoader.AuthStateDirectory);
        IReadOnlyList<LoginProfile> profiles = QytetarLoginPlanner.Resolve();
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === QYTETAR: login ne te njejtin browser me testin per {string.Join(", ", profiles.Select(p => SettingsLoader.AccountFor(p).Username))} ===");

        foreach (LoginProfile profile in profiles)
        {
            string statePath = SettingsLoader.AuthStatePath(profile);
            if (File.Exists(statePath))
                File.Delete(statePath);

            string nid = SettingsLoader.AccountFor(profile).Username;
            TestContext.Progress.WriteLine(
                $"{DateTime.Now:HH:mm:ss} | === QYTETAR: {nid} — login do te kryhet ne browserin e testit (OTP), jo ne nje shfletues te vecante ===");
        }
    }
}
