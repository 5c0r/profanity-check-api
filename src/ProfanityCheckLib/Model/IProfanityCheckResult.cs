using System;
using System.Collections.Generic;
using System.Text;

namespace ProfanityCheckLib.Model
{
    public interface IProfanityCheckResult
    {
        string UserId { get; set; }
        string FileId { get; set; }
        string Word { get; set; }
        int Position { get; set; }
    }
}
