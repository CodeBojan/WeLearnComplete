using F23.StringSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Shared.Services;
using Xunit.Abstractions;

namespace WeLearn.Shared.Tests.Services;

public class StringMatcherServiceTests
{
    private const string algoName = nameof(JaroWinkler);
    private readonly ITestOutputHelper output;

    public StringMatcherServiceTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void CanSelectAlgorithm()
    {
        var service = GetDefaultService();
        Assert.Equal(service.MainAlgorithmName, algoName);
    }

    [Theory]
    [InlineData("something")]
    public void CanRunAlgorithm(string s1)
    {
        var service = GetDefaultService();

        service.GetMatchPercentage(s1, s1);
    }

    [Theory]
    [InlineData("something")]
    public void CanFullMatch(string s1)
    {
        var service = GetDefaultService();

        Assert.Equal(100, service.GetMatchPercentage(s1, s1));
    }

    private static StringMatcherService GetDefaultService()
    {
        StringMatcherServiceSettings settings = GetServiceSettings();

        var service = new StringMatcherService(settings);
        return service;
    }

    private static StringMatcherServiceSettings GetServiceSettings()
    {
        return new StringMatcherServiceSettings
        {
            Algorithms = new() { algoName }
        };
    }
}
