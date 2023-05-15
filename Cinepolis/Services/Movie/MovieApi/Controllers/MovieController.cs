using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Services;
using MovieDomain.Models;
using MovieDomain.Services;
using System.Net;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IFileService _service;

        public MovieController(IFileService service, ILogger<MovieController> logger)
        {
            _logger = logger;
            _service = service;
        }



        [HttpGet("images-stream")]
        public IActionResult ReturnStream()
        {
            string filename = "Screenshot 2023-03-01 214032.png";
            Stream image = _service.GetFile(filename);

            byte[] a = ReadFully(image);
            
            return File(image, "image/png", filename);
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
                await _service.StoreFile(file.data, file.fileName);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
