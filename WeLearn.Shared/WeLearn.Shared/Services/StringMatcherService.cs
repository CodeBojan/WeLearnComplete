using F23.StringSimilarity;
using F23.StringSimilarity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Services;

public class StringMatcherService : IStringMatcherService
{
    private readonly StringMatcherServiceSettings _settings;
    private readonly List<INormalizedStringDistance> algorithms = new()
    {
            new NormalizedLevenshtein(),
            new JaroWinkler(),
            new Cosine(),
            new MetricLCS(),
            new NGram(),
            new Cosine(),
            new Jaccard(),
            new SorensenDice(),
            new RatcliffObershelp(),
        };

    public string MainAlgorithmName => algorithms.First().GetType().Name;

    protected INormalizedStringDistance MainAlgorithm => algorithms.First();

    public StringMatcherService(StringMatcherServiceSettings settings)
    {
        _settings = settings;
        algorithms = algorithms
            .Where(alg => 
                _settings.Algorithms.Contains(
                    alg.GetType().Name))
            .ToList();
    }

    public double GetMatchPercentage(string s1, string s2)
    {
        var distance = MainAlgorithm.Distance(s1, s2);
        var percentage = (1 - distance) * 100;
        
        return percentage;
    }
}
