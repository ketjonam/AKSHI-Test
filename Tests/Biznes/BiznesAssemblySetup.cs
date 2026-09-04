using AKSHI.Test.Core;

namespace AKSHI.Test.Tests.Biznes;

[SetUpFixture]
[NonParallelizable]
public sealed class BiznesAssemblySetup
{
    [OneTimeSetUp]
    public void PrepareBiznesAuth()
    {
        Directory.CreateDirectory(SettingsLoader.AuthStateDirectory);
        string statePath = SettingsLoader.AuthStatePath(LoginProfile.Biznes);
        if (File.Exists(statePath))
            File.Delete(statePath);

        AccountSettings account = SettingsLoader.AccountFor(LoginProfile.Biznes);
        TestContext.Progress.WriteLine(
            $"{DateTime.Now:HH:mm:ss} | === BIZNES: login me {account.Username} ne te njejtin browser me testin (OTP), jo ne nje shfletues te vecante ===");
    }
}
