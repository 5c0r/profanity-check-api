using Microsoft.AspNetCore.Http;

namespace ProfanityCheck.WebAPI.Model
{
    public sealed class FileInputModel
    {
        public IFormFile File { get; set; }
    }
}
