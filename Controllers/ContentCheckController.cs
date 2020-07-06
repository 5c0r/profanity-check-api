using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfanityCheckLib.Model;
using System;
using System.Collections.Generic;
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
                if(Files.File != null)
                {

                }
                else
                {
                    throw new InvalidOperationException("Invalid file uploaded");
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public sealed class FileInputModel
        {
            public IFormFile File { get; set; }
        }
    }
}
