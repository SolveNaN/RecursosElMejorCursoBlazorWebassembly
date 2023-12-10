using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DescargueArchivosClieteServidor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;


        public FileController(IWebHostEnvironment env)
        {
            _env = env;

        }
        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            //var uploadResult = await _context.UploadResults.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));

            var path = Path.Combine(_env.ContentRootPath, "Archivos", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "image/png", Path.GetFileName(path));
        }

    }
}
