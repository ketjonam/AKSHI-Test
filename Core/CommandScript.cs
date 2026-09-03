namespace AKSHI.Test.Core;

public static class CommandScript
{
    private static readonly char[] TokenSeparators = { '_', '-', '.', ' ', ',', ':', '/', '\\' };
    private static readonly Lazy<Dictionary<string, string>> KnownPathSegments = new(LoadKnownPathSegments);

    public static void Apply(CommandFilterSettings commands, string? script)
    {
        if (string.IsNullOrWhiteSpace(script))
            return;

        foreach (string raw in script.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            string line = StripComment(raw);
            if (line.Length == 0)
                continue;

            string[] parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length < 2)
                continue;

            string verb = Normalize(parts[0]);
            string target = Normalize(parts[1]);
            if (target.Length == 0)
                continue;

            if (verb == "run")
                AddUnique(commands.Run, target);
            else if (verb == "skip" && IsTestSelector(target))
                AddUnique(commands.Skip, target);
            else if (verb == "skip")
                AddUnique(commands.StepSkip, target);
        }
    }

    public static string? ToNUnitFilter(string? script)
    {
        var commands = new CommandFilterSettings();
        Apply(commands, script);
        if (commands.Run.Count == 0)
            return null;

        return string.Join("|", commands.Run.Select(ToFilterClause));
    }

    public static bool ShouldRunTest(IEnumerable<string> names, CommandFilterSettings commands)
    {
        string[] candidates = names
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .ToArray();

        if (candidates.Any(c => IsListed(commands.Skip, c)))
            return false;
        if (commands.Run.Count == 0)
            return true;
        return candidates.Any(c => IsListed(commands.Run, c));
    }

    public static bool Matches(string candidate, string filter)
    {
        if (string.IsNullOrWhiteSpace(candidate) || string.IsNullOrWhiteSpace(filter))
            return false;
        if (candidate.Equals(filter, StringComparison.OrdinalIgnoreCase))
            return true;

        string[] filterTokens = SplitTokens(filter);
        string[] candidateTokens = SplitTokens(candidate);
        if (filterTokens.Length == 0 || candidateTokens.Length == 0)
            return false;

        if (filterTokens.Length == 1)
        {
            if (candidateTokens.Any(token => token.Equals(filterTokens[0], StringComparison.OrdinalIgnoreCase)))
                return true;
        }
        else if (ContainsTokenSequence(candidateTokens, filterTokens))
            return true;

        return filter.Length >= 4
            && candidate.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }

    private static string ToFilterClause(string target)
    {
        if (target.Contains('.'))
        {
            string path = ToQualifiedPath(target);
            return $"FullyQualifiedName~{path}";
        }

        return $"Category={target}|FullyQualifiedName~{target}";
    }

    private static string ToQualifiedPath(string target) =>
        string.Join('.', target.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(ToPathSegment));

    private static string ToPathSegment(string segment)
    {
        string key = segment.Replace('-', '_');
        if (KnownPathSegments.Value.TryGetValue(key, out string? known)
            || KnownPathSegments.Value.TryGetValue(segment, out known))
            return known;

        if (segment.Length <= 4)
            return segment.ToUpperInvariant();
        return char.ToUpperInvariant(segment[0]) + segment[1..];
    }

    private static Dictionary<string, string> LoadKnownPathSegments()
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string institution in ServiceCatalog.All
                     .Select(s => s.Institution)
                     .Where(i => !string.IsNullOrWhiteSpace(i)))
        {
            string nsName = institution.Replace('-', '_');
            map.TryAdd(nsName, nsName);
            map.TryAdd(institution, nsName);
        }

        return map;
    }

    private static bool IsListed(IEnumerable<string> filters, string name) =>
        filters.Any(filter => Matches(name, filter));

    private static bool ContainsTokenSequence(string[] candidate, string[] filter)
    {
        int index = 0;
        foreach (string token in candidate)
        {
            if (!token.Equals(filter[index], StringComparison.OrdinalIgnoreCase))
                continue;
            index++;
            if (index == filter.Length)
                return true;
        }

        return false;
    }

    private static string[] SplitTokens(string value) =>
        value.Split(TokenSeparators, StringSplitOptions.RemoveEmptyEntries);

    private static bool IsTestSelector(string target) =>
        target.Contains('.')
        || target.StartsWith("tests", StringComparison.OrdinalIgnoreCase);

    private static string StripComment(string line)
    {
        int hash = line.IndexOf('#');
        int slash = line.IndexOf("//", StringComparison.Ordinal);
        int cut = hash >= 0 && slash >= 0 ? Math.Min(hash, slash)
            : hash >= 0 ? hash
            : slash;
        return cut >= 0 ? line[..cut].Trim() : line.Trim();
    }

    private static string Normalize(string value) =>
        value.Replace('ë', 'e').Replace('Ë', 'E').Trim();

    private static void AddUnique(List<string> target, string value)
    {
        if (!target.Contains(value, StringComparer.OrdinalIgnoreCase))
            target.Add(value);
    }
}
