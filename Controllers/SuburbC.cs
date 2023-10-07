using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuburbC : ControllerBase
    {
        private readonly SuburbIRepository _suburbIRepository;

        public SuburbC(SuburbIRepository suburbIRepository)
        {
            _suburbIRepository = suburbIRepository;
        }

        [HttpGet]
        [Route("GetSuburb")]
        public async Task<IActionResult> GetSuburb()
        {
            try
            {
                var results = await _suburbIRepository.GetSuburbAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{SuburbId:Guid}")]
        public async Task<IActionResult> FetchSuburb([FromRoute] Guid SuburbId)
        {
            var suburb = await _suburbIRepository.GetSuburb(SuburbId);
            return Ok(suburb);
        }

        [HttpPost]
        [Route("AddSuburb")]
        public async Task<IActionResult> Post(Suburb suburb)
        {

            try
            {
                var result = await _suburbIRepository.AddSuburb(suburb);
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Suburb name already exists" });
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
        [Route("EditSuburb")]
        public async Task<IActionResult> Put(Guid Id, Suburb editedSuburb)
        {
            editedSuburb.SuburbId = Id;
            await _suburbIRepository.EditSuburb(editedSuburb);
            return Ok(new { message = "Suburb edited successfully" });
        }


        [HttpDelete]
        [Route("DeleteSuburb/{id}")]
        public JsonResult DeleteSuburb(Guid id)
        {
            _suburbIRepository.DeleteSuburb(id);
            return new JsonResult("Suburb deleted successfully");
        }
    }
}
