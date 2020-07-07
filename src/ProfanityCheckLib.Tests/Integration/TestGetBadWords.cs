using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ProfanityCheckLib.Tests.Integration
{
    public sealed  class TestGetBadWords
    {
        [Fact]
        public async Task CanGetBadWordsFromRawGithub()
        {
            using (var client = new HttpClient())
            {
                var response = await client
    .GetAsync("https://raw.githubusercontent.com/chucknorris-io/swear-words/master/fi", HttpCompletionOption.ResponseHeadersRead);

                var contentType = response.Content.Headers.ContentType.MediaType;

                contentType.Should().Be("text/plain");

                if (contentType.Equals("text/plain"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    stringResponse.Should().NotBeNullOrEmpty();

                    // \n is the split character, Environment.NewLine should work OOTB for Linux, but not Windows :(
                    var parsedStrings = stringResponse.Split("\n");
                    parsedStrings.Length.Should().BeGreaterThan(1);
                }
            }

        }
    }
}
