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
    public class ProvinceC : ControllerBase
    {
        private readonly ProvinceIRepository _repository;

        public ProvinceC(ProvinceIRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetProvinces")]
        public async Task<IActionResult> GetProvinces()
        {
            try
            {
                var results = await _repository.GetProvinceAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{ProvinceId:Guid}")]
        public async Task<IActionResult> FetchProvince([FromRoute] Guid ProvinceId)
        {
            var province = await _repository.GetProvince(ProvinceId);
            return Ok(province);
        }

        [HttpPost]
        [Route("AddProvince")]
        public async Task<IActionResult> Post(Province province)
        {


            try
            {
                var result = await _repository.AddProvince(province);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Province name already exists" });
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
        [Route("EditProvince")]
        public async Task<IActionResult> Put(Guid Id, Province editedProvince)
        {
            editedProvince.ProvinceId = Id;
            await _repository.EditProvince(editedProvince);
            return Ok(new { message = "Province edited successfully" });
        }


        [HttpDelete]
        [Route("DeleteProvince/{id}")]
        public IActionResult DeleteProvince(Guid id)
        {
            bool deletionResult = _repository.DeleteProvince(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete province. It may be referenced by existing cities.");
            }
        }


    }
}
