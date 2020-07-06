using Microsoft.AspNetCore.Mvc;
using ProfanityCheckLib.Model;

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
        public IActionResult CheckContentDocument([FromBody]object something)
        {
            return Ok();
        }
    }
}
