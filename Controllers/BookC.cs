using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Architecture.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookC : ControllerBase
    {
        private readonly BookIRepository _bookIRepository;

        public BookC(BookIRepository bookIRepository)
        {
            _bookIRepository = bookIRepository;
        }


        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBook()
        {
            try
            {
                var results = await _bookIRepository.GetBooksAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{BookId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid BookId)
        {
            var Type = await _bookIRepository.GetBook(BookId);
            return Ok(Type);
        }

        [HttpGet]
        [Route("GetCheckout")]
        public async Task<IActionResult> GetCheckout()
        {
            try
            {
                var results = await _bookIRepository.GetCheckOutAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> PostType(Book books)
        {

            try
            {
                var result = await _bookIRepository.AddBook(books);
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
        [Route("EditBook")]
        public async Task<IActionResult> PutType(Guid Id, Book editBooks)
        {
            editBooks.BookId = Id;
            await _bookIRepository.EditBook(editBooks);
            return Ok(new { message = "Book  edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public JsonResult DeleteBook(Guid id)
        {
            _bookIRepository.DeleteBook(id);
            return new JsonResult("Book deleted successfully");
        }

        [HttpPost("CheckoutBook")]
        public async Task<IActionResult> CheckoutBook([FromBody] CheckOut checkout)
        {
            try
            {
                // Save the checkout details to the CheckOut table
                await _bookIRepository.CheckOutBook(checkout);

                // Update the book's status to "unavailable" in the Book table
                var book = await _bookIRepository.GetBook(checkout.BookId);
                if (book != null)
                {
                    book.BookStatusId = new Guid("D783823A-29CB-4D00-A861-08DBABA2957E"); // Update with the actual ID of "unavailable" status
                    await _bookIRepository.EditBook(book);
                }

                return Ok(new { message = "Book checked out successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("CheckinBook")]
        public async Task<IActionResult> CheckinBook([FromBody] CheckIn checkin)
        {
            try
            {
                // Save the checkout details to the CheckOut table
                await _bookIRepository.CheckInBook(checkin);

                // Update the book's status to "unavailable" in the Book table
                var book = await _bookIRepository.GetBook(checkin.BookId);
                if (book != null)
                {
                    book.BookStatusId = new Guid("ee3ba3b1-22aa-4e68-fdc1-08dbac97c445"); // Update with the actual ID of "available" status
                    await _bookIRepository.EditBook(book);
                }

                return Ok(new { message = "Book checked-in successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCheckout/{id}")]
        public JsonResult DeleteCheckout(Guid id)
        {
            _bookIRepository.DeleteCheckout(id);
            return new JsonResult("Checkout deleted successfully");
        }

    }
}
