using Microsoft.AspNetCore.Mvc;

namespace zltNewsletter.Controllers
{
    public class ImageController : Controller
    {
   
        private Microsoft.Extensions.Hosting.IHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageController(IHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("Image/UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Change this to physical location where files end
            var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\mceupload", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var CurrentUrl = _httpContextAccessor.HttpContext.Request.Scheme +
                             "://" + _httpContextAccessor.HttpContext.Request.Host;

            // Change this to right URL where files ends - will be inserted in TinyMce html
            var FileDestination = CurrentUrl + "/mceupload/" + file.FileName;

            var response = new { Location = FileDestination };
            return Ok(response);
        }
    }
}
