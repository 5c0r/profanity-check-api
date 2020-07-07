using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfanityCheck.WebAPI.Model
{
    public class ErrorResponse : ICommonResponse<string>
    {
        public bool Success => false;
        public string Data { get; set; }
    }
}
