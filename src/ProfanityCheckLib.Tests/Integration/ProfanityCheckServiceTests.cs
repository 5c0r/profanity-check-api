using FluentAssertions;
using ProfanityCheckLib.Service;
using Xunit;

namespace ProfanityCheckLib.Tests.Integration
{
    public sealed class ProfanityCheckServiceTests
    {
        private readonly ProfanityCheckService serviceUnderTests;

        // TODO: This can be intialized using TestFixture
        public ProfanityCheckServiceTests()
        {
            this.serviceUnderTests = new ProfanityCheckService();
            this.serviceUnderTests.AddNewBannedWordsFromGitHub("https://raw.githubusercontent.com/chucknorris-io/swear-words/master/fi");
            this.serviceUnderTests.AddNewBannedWordsFromGitHub("https://raw.githubusercontent.com/chucknorris-io/swear-words/master/en");
        }

        [Fact]
        public void ShouldWork()
        {
            this.serviceUnderTests.Should().NotBeNull();
            this.serviceUnderTests.BannedWords.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void CanCheckProfanityUsingLINQ()
        {
            this.serviceUnderTests.CheckWithLINQ("Hello").Should()
                .BeFalse("Hello is a friendly hello");

            this.serviceUnderTests.CheckWithLINQ("VITTU").Should()
                .BeTrue("It's a bad word, and it's even in uppercase");
        }

        [Fact]
        public void CanCheckProfanityUsingRegex()
        {
            this.serviceUnderTests.CheckWithRegex("Hello").Should()
                .BeFalse("Hello is a friendly hello");

            this.serviceUnderTests.CheckWithRegex("VITTU").Should()
                .BeTrue("It's a bad word, and it's even in uppercase");

            this.serviceUnderTests.CheckWithRegex("Mita helvetisti ?").Should()
                .BeTrue("It's a bad word, and it's even in uppercase");

        }

        [Fact]
        public void CanGetViolatedWords()
        {
            // Regex could be faster , but return results are different while having duplicate entries
            string textContent = "Mita helvetisti shit shit ?";

            var violatedWords = this.serviceUnderTests.GetViolatedWords(textContent);
            var violatedWordsWithRegex = this.serviceUnderTests.GetViolatedWordsWithRegex(textContent);

            violatedWords.Should().BeEquivalentTo(violatedWordsWithRegex);
        }
    }
}
