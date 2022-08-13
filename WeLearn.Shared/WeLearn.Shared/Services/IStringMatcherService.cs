namespace WeLearn.Shared.Services
{
    public interface IStringMatcherService
    {
        double GetMatchPercentage(string s1, string s2);
    }
}