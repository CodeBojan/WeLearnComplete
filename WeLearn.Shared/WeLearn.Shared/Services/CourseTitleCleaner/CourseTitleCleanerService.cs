using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cyrillic.Convert;
using Microsoft.Extensions.Options;

namespace WeLearn.Shared.Services.CourseTitleCleaner;

public class CourseTitleCleanerService : ICourseTitleCleanerService
{
    private CourseTitleCleanerServiceSettings settings;
    private readonly IOptionsMonitor<CourseTitleCleanerServiceSettings> _settingsMonitor;
    private Dictionary<Regex, string> regexReplaceDict = new();

    public CourseTitleCleanerService(IOptionsMonitor<CourseTitleCleanerServiceSettings> settingsMonitor)
    {
        _settingsMonitor = settingsMonitor;
        InitializeSettings();
    }

    public string Cleanup(string input)
    {
        var dirty = input
            .Trim()
            .ToUpper();

        var regexed = ApplyRegex(dirty);

        string transformed = regexed;

        if (settings.ShouldConvertToLatin)
            transformed = regexed.ToSerbianLatin();
        else if (settings.ShouldConvertToCyrillic)
            transformed = regexed.ToSerbianCyrilic();

        var result = transformed;

        return result;
    }

    private string ApplyRegex(string input)
    {
        InitializeSettings();

        string result = input;

        foreach (var regexPair in regexReplaceDict)
        {
            var regex = regexPair.Key;
            var replaceWith = regexPair.Value;
            result = regex.Replace(result, replaceWith);
        }

        return result;
    }

    private void InitializeSettings()
    {
        var currentSettings = _settingsMonitor.CurrentValue;
        if (currentSettings != settings)
        {
            lock (this)
            {
                settings = _settingsMonitor.CurrentValue;
                regexReplaceDict = new Dictionary<Regex, string>(
                    settings.Regexes.Select(p =>
                    new KeyValuePair<Regex, string>(
                        new Regex(p.Key), p.Value)));
            }
        }
    }
}
