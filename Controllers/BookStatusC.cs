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
    public class BookStatusC : ControllerBase
    {
        private readonly BookStatusIRepository _bookStatusIRepository;

        public BookStatusC(BookStatusIRepository bookStatusIRepository)
        {
            _bookStatusIRepository = bookStatusIRepository;
        }


        [HttpGet]
        [Route("GetBookStatus")]
        public async Task<IActionResult> GetBookGenre()
        {
            try
            {
                var results = await _bookStatusIRepository.GetBookStatusAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{StatusId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid StatusId)
        {
            var Type = await _bookStatusIRepository.GetBookStatus(StatusId);
            return Ok(Type);
        }


        [HttpPost]
        [Route("AddBookStatus")]
        public async Task<IActionResult> PostType(BookStatus status)
        {

            try
            {
                var result = await _bookStatusIRepository.AddBookStatus(status);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Book status already exists" });
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
        [Route("EditBookStatus")]
        public async Task<IActionResult> PutType(Guid Id, BookStatus editStatus)
        {
            editStatus.BookStatusId = Id;
            await _bookStatusIRepository.EditBookStatus(editStatus);
            return Ok(new { message = "Book Status edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteBookStatus/{id}")]
        public IActionResult DeleteBookStatus(Guid id)
        {
            bool deletionResult = _bookStatusIRepository.DeleteBookStatus(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete book status. It may be referenced by existing books.");
            }
        }
    }
}
