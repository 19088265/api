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
    public class CafeteriaTypeC : ControllerBase
    {
        private readonly CafeteriaTypeIRepository _cafeteriaTypeIRepository;

        public CafeteriaTypeC(CafeteriaTypeIRepository cafeteriaTypeIRepository)
        {
            _cafeteriaTypeIRepository = cafeteriaTypeIRepository;
        }

        [HttpGet]
        [Route("GetCafeteriaTypes")]
        public async Task<IActionResult> GetCafeteriaType()
        {
            try
            {
                var results = await _cafeteriaTypeIRepository.GetCafeteriaTypeAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{TypeId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid TypeId)
        {
            var Type = await _cafeteriaTypeIRepository.GetCafeteriaType(TypeId);
            return Ok(Type);
        }

        [HttpPost]
        [Route("AddCafeteriaType")]
        public async Task<IActionResult> PostType(CafeteriaType cafeType)
        {

            try
            {
                var result = await _cafeteriaTypeIRepository.AddCafeteriaType(cafeType);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Cafeteria type already exists" });
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
        [Route("EditCafeteriaType")]
        public async Task<IActionResult> PutType(Guid Id, CafeteriaType editedCafeType)
        {
            editedCafeType.CafeteriaTypeId = Id;
            await _cafeteriaTypeIRepository.EditCafeteriaType(editedCafeType);
            return Ok(new { message = "Edited Successfully" });
        }

        [HttpDelete]
        [Route("DeleteCafeteriaType/{id}")]
        public IActionResult DeleteSponsorType(Guid id)
        {
            bool deletionResult = _cafeteriaTypeIRepository.DeleteCafeteriaType(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete CafeteriaType. It may be referenced by existing cafeterias.");
            }
        }
    }
}
