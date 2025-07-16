using Microsoft.AspNetCore.Mvc;

namespace ImageSurveyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost]
        public IActionResult Upload()
        {
            return Ok(new { message = "Upload endpoint is working!" });
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { message = "Upload endpoint is reachable by GET (for test only)." });
        }


    }
}
