using F23.StringSimilarity;
using F23.StringSimilarity.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WeLearn.Shared.Tests")]
namespace WeLearn.Shared.Services.StringMatcher;

public class StringMatcherService : IStringMatcherService
{
    private readonly StringMatcherServiceSettings _settings;
    private List<INormalizedStringDistance> algorithms = new()
    {
            new NormalizedLevenshtein(),
            new JaroWinkler(),
            //new Cosine(), // TODO
            //new MetricLCS(),
            //new NGram(),
            //new Cosine(),
            //new Jaccard(),
            //new SorensenDice(),
            //new RatcliffObershelp(),
        };

    public string MainAlgorithmName => algorithms.First().GetType().Name;

    protected INormalizedStringDistance MainAlgorithm => algorithms.First();

    internal StringMatcherService(StringMatcherServiceSettings settings)
    {
        _settings = settings;
        InitializeAlgorithms();
    }

    public StringMatcherService(IOptions<StringMatcherServiceSettings> settingOptions)
    {
        _settings = settingOptions.Value;
        InitializeAlgorithms();
    }

    public double GetMatchPercentage(string s1, string s2)
    {
        var distance = MainAlgorithm.Distance(s1, s2);
        var percentage = (1 - distance) * 100;

        return percentage;
    }

    public void InitializeAlgorithms()
    {
        algorithms = algorithms
           .Where(alg =>
               _settings.Algorithms.Contains(
                   alg.GetType().Name))
           .ToList();
    }
}
