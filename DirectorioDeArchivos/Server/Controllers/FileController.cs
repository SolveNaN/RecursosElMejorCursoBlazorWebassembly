using DirectorioDeArchivos.Server.Data;
using DirectorioDeArchivos.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using DirectorioDeArchivos.Server.Data;
using DirectorioDeArchivos.Shared;

namespace DirectorioDeArchivos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public FileController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.Nombre = untrustedFileName;
                //var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

                trustedFileNameForFileStorage = file.FileName;
                var path = Path.Combine(_env.ContentRootPath, "uploads", trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);
                uploadResult.Nombre = "Hola";
                uploadResult.StoredFileName = trustedFileNameForFileStorage;
                uploadResult.ContentType = file.ContentType;
                uploadResults.Add(uploadResult);

                _context.UploadResults.Add(uploadResult);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();

            return Ok(uploadResults);
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            //var uploadResult = await _context.UploadResults.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));

            var path = Path.Combine(_env.ContentRootPath, "uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
           //return File(memory, "image/png", Path.GetFileName(path));//para imagenes pnd
           return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(path));//Para excel


        }

    }
}
