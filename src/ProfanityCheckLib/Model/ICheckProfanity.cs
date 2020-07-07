using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProfanityCheckLib.Model
{
    public interface ICheckProfanity
    {
        HashSet<string> AcceptedExtensions { get; }
        IReadOnlyList<string> BannedWords { get; }
        Regex BannedWordsRegex { get; }

        public bool CheckWithLINQ(string text);
        public bool CheckWithRegex(string text);
        public IReadOnlyList<string> GetViolatedWords(string text);
        public IReadOnlyList<string> GetViolatedWordsWithRegex(string text);

    }
}
