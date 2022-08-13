namespace WeLearn.Shared.Services.StringMatcher
{
    public interface IStringMatcherService
    {
        double GetMatchPercentage(string s1, string s2);
    }
}