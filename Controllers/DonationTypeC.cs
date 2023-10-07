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
    public class DonationTypeC : ControllerBase
    {
        private readonly DonationTypeIRepository _donationTypeIRepository;

        public DonationTypeC(DonationTypeIRepository donationTypeIRepository)
        {
            _donationTypeIRepository = donationTypeIRepository;
        }

        [HttpGet]
        [Route("GetDonationTypes")]
        public async Task<IActionResult> GetDonationType()
        {
            try
            {
                var results = await _donationTypeIRepository.GetDonationTypeAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpGet]
        [Route("{DonationTypeId:Guid}")]
        public async Task<IActionResult> FetchDonationType([FromRoute] Guid DonationTypeId)
        {
            var type = await _donationTypeIRepository.GetDonationType(DonationTypeId);
            return Ok(type);
        }

        [HttpPost]
        [Route("AddDonationType")]
        public async Task<IActionResult> PostType(DonationType donationType)
        {

            try
            {
                var result = await _donationTypeIRepository.AddDonationType(donationType);
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
        [Route("EditDonationType")]
        public async Task<IActionResult> PutType(Guid Id, DonationType editedDonationType)
        {
            editedDonationType.DonationTypeId = Id;
            await _donationTypeIRepository.EditDonationType(editedDonationType);
            return Ok(new { message = "DonationType edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteDonationType/{id}")]
        public IActionResult DeleteDonationType(Guid id)
        {
            bool deletionResult = _donationTypeIRepository.DeleteDonationType(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete DonationType. It may be referenced by existing donations.");
            }
        }
    }
}
