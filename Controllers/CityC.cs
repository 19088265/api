using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Architecture.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityC : ControllerBase
    {
        private readonly CityIRepository _cityIRepository;

        public CityC(CityIRepository cityIRepository)
        {
            _cityIRepository = cityIRepository;
        }

        [HttpGet]
        [Route("GetCity")]
        public async Task<IActionResult> GetCity()
        {
            try
            {
                var results = await _cityIRepository.GetCityAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{CityId:Guid}")]
        public async Task<IActionResult> FetchCity([FromRoute] Guid CityId)
        {
            var city = await _cityIRepository.GetCity(CityId);
            return Ok(city);
        }

        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> Post(City city)
        {

            try
            {
                var result = await _cityIRepository.AddCity(city);
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "City name already exists" });
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
        [Route("EditCity")]
        public async Task<IActionResult> Put(Guid Id, City editedCity)
        {
            editedCity.CityId = Id;
            await _cityIRepository.EditCity(editedCity);
            return Ok(new { message = "City edited successfully" });
        }



        [HttpDelete]
        [Route("DeleteCity/{id}")]
        public IActionResult DeleteCity(Guid id)
        {
            bool deletionResult = _cityIRepository.DeleteCity(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete city. It may be referenced by existing suburbs.");
            }
        }
    }
}
