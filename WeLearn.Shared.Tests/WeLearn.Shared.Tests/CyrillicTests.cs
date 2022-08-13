using Cyrillic.Convert;

namespace WeLearn.Shared.Tests
{
    public class CyrillicTests
    {
        [Fact]
        public void CanCompareCyrillicAndLatin()
        {
            Assert.Equal("abcšđčćžnjljdž", "абцшђчћжњљџ".ToSerbianLatin());
        }
    }
}