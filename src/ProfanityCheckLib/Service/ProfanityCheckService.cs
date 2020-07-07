using ProfanityCheckLib.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProfanityCheckLib.Service
{
    public sealed class ProfanityCheckService : ICheckProfanity
    {
        public HashSet<string> AcceptedExtensions => new HashSet<string>()
        {
            "txt", "csv", "log"
        };

        public IReadOnlyList<string> BannedWords => _bannedWords
            .Where(word => !string.IsNullOrEmpty(word))
            .Distinct()
            .ToImmutableList();
        public Regex BannedWordsRegex => new Regex(@"\b(" + string.Join("|", BannedWords) + @"\b)", RegexOptions.IgnoreCase);

        private readonly List<string> _bannedWords = new List<string>();

        // TODO: It is possible to inject IHttpClient, but we won't do that here , yet
        public ProfanityCheckService()
        {

        }

        public void AddNewBannedWord(string word)
        {
            this._bannedWords.Add(word);
        }

        public void AddNewBannedWordsFromGitHub(string url)
        {
            this._bannedWords.AddRange(GetBannedWordsFromGithub(url).Result);
        }

        public void AddBannedWordsFromTextFile(string filePath)
        {
            var stringContent = File.ReadAllText(filePath, Encoding.UTF8);
            var parsedString = stringContent.Split(Environment.NewLine);

            this._bannedWords.AddRange(parsedString);
        }

        private async Task<IList<string>> GetBannedWordsFromGithub(string githubUrl)
        {
            using (var client = new HttpClient())
            {
                var response = await client
                    .GetAsync(githubUrl, 
                    HttpCompletionOption.ResponseHeadersRead);

                var contentType = response.Content.Headers.ContentType.MediaType;

                if (contentType.Equals("text/plain"))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    var parsedStrings = stringResponse.Split("\n");

                    return parsedStrings.ToList();
                }
                return Array.Empty<string>().ToList();
            }
        }

        public bool CheckWithLINQ(string text)
        {
            this.GuardIfDictionaryEmpty();

            return this.BannedWords.Any(word => 
                text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public bool CheckWithRegex(string text)
        {
            this.GuardIfDictionaryEmpty();

            return BannedWordsRegex.IsMatch(text);
        }

        public IReadOnlyList<string> GetViolatedWords(string text)
        {
            this.GuardIfDictionaryEmpty();

            return this.BannedWords.Where(word => text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToImmutableList();
        }

        public IReadOnlyList<string> GetViolatedWordsWithRegex(string text)
        {
            this.GuardIfDictionaryEmpty();

            return BannedWordsRegex.Matches(text).Select(x => x.Value).Distinct().ToImmutableList();
        }

        private void GuardIfDictionaryEmpty()
        {
            if (this.BannedWords.Count == 0)
                throw new InvalidOperationException("No banned words added to dictionary");
        }
    }
}
