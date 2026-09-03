using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace AKSHI.Test.Core;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public sealed class TestRunFilterAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(NUnit.Framework.Internal.Test test)
    {
        CommandFilterSettings commands = SettingsLoader.Current.Commands;
        if (commands.Run.Count == 0 && commands.Skip.Count == 0)
            return;

        if (CommandScript.ShouldRunTest(NamesOf(test), commands))
            return;

        test.RunState = RunState.Ignored;
        test.Properties.Set(PropertyNames.SkipReason,
            "Anashkaluar nga TestRun.cs (nuk eshte ne listen run).");
    }

    private static IEnumerable<string> NamesOf(NUnit.Framework.Internal.Test test)
    {
        yield return test.Name;
        yield return test.FullName;
        yield return test.ClassName ?? string.Empty;

        if (test.TypeInfo?.FullName is string typeName)
            yield return typeName;

        if (!test.Properties.ContainsKey("Category"))
            yield break;

        foreach (object? category in test.Properties["Category"])
        {
            if (category is not null)
                yield return category.ToString() ?? string.Empty;
        }
    }
}
