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
    public class BeneficiaryC : ControllerBase
    {
        private readonly BeneficiaryIRepository _beneficiaryIRepository;

        public BeneficiaryC(BeneficiaryIRepository beneficiaryIRepository)
        {
            _beneficiaryIRepository = beneficiaryIRepository;
        }

        [HttpGet]
        [Route("GetBeneficiaries")]
        public async Task<IActionResult> GetBeneficiaries()
        {
            try
            {
                var results = await _beneficiaryIRepository.GetBeneficiaryAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{BeneficiaryId:Guid}")]
        public async Task<IActionResult> FetchBeneficiary([FromRoute] Guid BeneficiaryId)
        {
            var beneficiary = await _beneficiaryIRepository.GetBeneficiary(BeneficiaryId);
            return Ok(beneficiary);
        }

        //Add Beneficiary
        [HttpPost]
        [Route("AddBeneficiary")]
        public async Task<IActionResult> Post(Beneficiary beneficiary)
        {

            try
            {
                var result = await _beneficiaryIRepository.AddBeneficiary(beneficiary);
                return Ok(new { message = "Added successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Id Number already exists" });
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

        //Edit Beneficiary
        [HttpPut]
        [Route("EditBeneficiary")]
        public async Task<IActionResult> Update(Guid id, Models.Beneficiary beneficiaryx)
        {
            //await _beneficiaryIRepository.EditBeneficiary(beneficiaryx);
            //return Ok("Beneficiary edited successfully");

            beneficiaryx.BeneficiaryId = id;
            await _beneficiaryIRepository.EditBeneficiary(beneficiaryx);
            return Ok(new { message = "Beneficiary edited successfully" });
        }
        //Delete Beneficiary


        [HttpDelete]
        [Route("DeleteBeneficiary/{id}")]
        public IActionResult DeleteBeneficiary(Guid id)
        {
            bool deletionResult = _beneficiaryIRepository.DeleteBeneficiary(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete Beneficiary. It may be referenced by existing books.");
            }
        }

    }
}
