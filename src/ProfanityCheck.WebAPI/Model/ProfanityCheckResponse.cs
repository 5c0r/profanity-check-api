using ProfanityCheckLib.Model;
using System;

namespace ProfanityCheck.WebAPI.Model
{
    public sealed class ProfanityCheckResponse : ICommonResponse<ProfanityCheckValue>
    {
        public bool Success { get; set; }
        public ProfanityCheckValue Data { get; set; }
    }
}
