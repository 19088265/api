using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeC : ControllerBase
    {
        private readonly PaymentTypeIRepository _paymentTypeIRepository;

        public PaymentTypeC(PaymentTypeIRepository paymentTypeIRepository)
        {
            _paymentTypeIRepository = paymentTypeIRepository;
        }

        [HttpGet]
        [Route("GetPaymentTypes")]
        public async Task<IActionResult> GetPaymentType()
        {
            try
            {
                var results = await _paymentTypeIRepository.GetPaymentTypeAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpGet]
        [Route("{PaymentTypeId:Guid}")]
        public async Task<IActionResult> FetchPaymentType([FromRoute] Guid PaymentTypeId)
        {
            var type = await _paymentTypeIRepository.GetPaymentType(PaymentTypeId);
            return Ok(type);
        }

        [HttpPost]
        [Route("AddPaymentType")]
        public async Task<IActionResult> PostType(PaymentType paymentType)
        {

            try
            {
                var result = await _paymentTypeIRepository.AddPaymentType(paymentType);
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Payment type already exists" });
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
        [Route("EditPaymentType")]
        public async Task<IActionResult> PutType(Guid Id, PaymentType editedPaymentType)
        {
            editedPaymentType.PaymentTypeId = Id;
            await _paymentTypeIRepository.EditPaymentType(editedPaymentType);
            return Ok(new { message = "PaymentType edited successfully" });
        }

        [HttpDelete]
        [Route("DeletePaymentType/{id}")]
        public IActionResult DeletePaymentType(Guid id)
        {
            bool deletionResult = _paymentTypeIRepository.DeletePaymentType(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete PaymentType. It may be referenced by existing payments.");
            }
        }
    }
}
