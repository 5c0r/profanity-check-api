using System.Collections.Generic;

namespace ProfanityCheckLib.Model
{
    public sealed class ProfanityCheckValue
    {
        public IList<string> ViolatedWords { get; set; }
    }
}
