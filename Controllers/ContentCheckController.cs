using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfanityCheck.WebAPI.Model;
using ProfanityCheckLib.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProfanityCheck.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentCheckController : ControllerBase
    {
        private readonly ICheckProfanity profanityCheckSvc;

        public ContentCheckController(ICheckProfanity profanityCheckSvc)
        {
            this.profanityCheckSvc = profanityCheckSvc;
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {
            var allBadwords = this.profanityCheckSvc.BannedWords;

            return Ok(allBadwords);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult CheckContentDocument([FromForm] FileInputModel Files, CancellationToken cancellationToken)
        {
            try
            {
                if (Files.File == null) 
                    throw new Exception("No File was uploaded");
                if (!this.profanityCheckSvc.AcceptedExtensions.Any(ext => Files.File.FileName.EndsWith(ext)))
                    throw new Exception($"Invalid file extensions. " +
                        $"Expected ${string.Join(',', this.profanityCheckSvc.AcceptedExtensions.ToArray())}.");

                using(MemoryStream ms = new MemoryStream())
                {
                    Files.File.CopyTo(ms);
                    var textContent = Encoding.UTF8.GetString(ms.ToArray());

                    var result = this.profanityCheckSvc.GetViolatedWords(textContent);

                    return Ok(new ProfanityCheckResponse()
                    {
                        Success = true,
                        Data = new ProfanityCheckValue()
                        {
                           ViolatedWords = result.ToList()
                        }
                    }) ;
                }
            }
            catch (Exception ex)
            {
                return Ok(new ErrorResponse()
                {
                    Data = ex.Message
                });
            }

        }
    }
}
