using System.Text.Json;
using System.Text.Json.Serialization;

namespace AKSHI.Test.Core;

public sealed class ServiceInfo
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string ProfileType { get; set; } = "Individual";
    public string LoginProfile { get; set; } = "Qytetar";
    public string Search { get; set; } = string.Empty;
    public string StartMode { get; set; } = "NewApplication";
    public string SourceFile { get; set; } = string.Empty;

    [JsonIgnore]
    public LoginProfile Login =>
        string.Equals(LoginProfile, "Biznes", StringComparison.OrdinalIgnoreCase)
            ? Core.LoginProfile.Biznes
            : Core.LoginProfile.Qytetar;

    [JsonIgnore]
    public ServiceStartMode Mode =>
        string.Equals(StartMode, "Track", StringComparison.OrdinalIgnoreCase)
            ? ServiceStartMode.Track
            : ServiceStartMode.NewApplication;

    [JsonIgnore]
    public string SearchTerm =>
        string.IsNullOrWhiteSpace(Search) ? (string.IsNullOrWhiteSpace(Title) ? Code : Title) : Search;
}

public static class ServiceCatalog
{
    private static readonly Lazy<IReadOnlyDictionary<string, List<ServiceInfo>>> Cached = new(Load);

    public static IReadOnlyList<ServiceInfo> All =>
        Cached.Value.Values.SelectMany(x => x).ToList();

    public static ServiceInfo Resolve(string code, string? testName = null)
    {
        if (!Cached.Value.TryGetValue(code, out List<ServiceInfo>? matches) || matches.Count == 0)
        {
            return new ServiceInfo
            {
                Code = code,
                Title = testName ?? code,
                Search = testName ?? code
            };
        }

        if (!string.IsNullOrWhiteSpace(testName))
        {
            ServiceInfo? named = matches.FirstOrDefault(m =>
                string.Equals(m.Title, testName, StringComparison.OrdinalIgnoreCase));
            if (named is not null)
                return named;
        }

        return matches[0];
    }

    private static IReadOnlyDictionary<string, List<ServiceInfo>> Load()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Config", "services.json");
        if (!File.Exists(path))
            return new Dictionary<string, List<ServiceInfo>>();

        string json = File.ReadAllText(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<ServiceInfo> items = JsonSerializer.Deserialize<List<ServiceInfo>>(json, options) ?? new();

        return items
            .GroupBy(x => x.Code, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);
    }
}
