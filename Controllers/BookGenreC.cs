using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Architecture.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookGenreC : ControllerBase
    {
        private readonly BookGenreIRepository _bookGenreIRepository;

        public BookGenreC(BookGenreIRepository bookGenreIRepository)
        {
            _bookGenreIRepository = bookGenreIRepository;
        }


        [HttpGet]
        [Route("GetBookGenres")]
        public async Task<IActionResult> GetBookGenre()
        {
            try
            {
                var results = await _bookGenreIRepository.GetBookGenreAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{GenreId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid GenreId)
        {
            var Type = await _bookGenreIRepository.GetBookGenre(GenreId);
            return Ok(Type);
        }







        [HttpPost]
        [Route("AddBookGenre")]
        public async Task<IActionResult> PostType(BookGenre genre)
        {

            try
            {
                var result = await _bookGenreIRepository.AddBookGenre(genre);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Book genre already exists" });
                }
                else
                {
                    // Handle other database-related errors
                    return StatusCode(500, "Internal Server Error");
                }
            }

        }

        private bool IsUniqueConstraintViolation(Exception exception)
        {
            if (exception is DbUpdateException updateException && updateException.InnerException is SqlException sqlException)
            {
                // SQL Server error number for unique constraint violation is 2601
                return sqlException.Number == 2601;
            }
            return false;
        }

        [HttpPut]
        [Route("EditBookGenre")]
        public async Task<IActionResult> PutType(Guid Id, BookGenre editGenre)
        {
            editGenre.BookGenreId = Id;
            await _bookGenreIRepository.EditBookGenre(editGenre);
            return Ok(new { message = "Book Genre edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteBookGenre/{id}")]
        public IActionResult DeleteBookGenre(Guid id)
        {
            bool deletionResult = _bookGenreIRepository.DeleteBookGenre(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete book genre. It may be referenced by existing books.");
            }
        }
    }
}
