using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadC : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadC(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("searchFile/{filename}")]
        public IActionResult SearchFile(string filename)
        {
            // Implement the logic to search for the file based on the provided filename
            // If the file is found, generate a URL to access the file and return it
            // If the file is not found, return a response indicating that the file was not found

            // Example: Search for a file in the "uploads" directory
            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");
            var filePath = Path.Combine(uploadPath, filename);

            if (System.IO.File.Exists(filePath))
            {
                // Read the file content as bytes
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Return the file content with the appropriate "Content-Type" header
                return File(fileBytes, "application/pdf", filename);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine("Uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { message = "File uploaded successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading file: {ex.Message}");
            }

        }
    }
}
