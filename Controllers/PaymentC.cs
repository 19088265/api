using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentC : ControllerBase
    {
        private readonly PaymentIRepository _paymentIRepository;

        public PaymentC(PaymentIRepository paymentIRepository)
        {
            _paymentIRepository = paymentIRepository;
        }

        [HttpGet]
        [Route("GetPayment")]
        public async Task<IActionResult> GetPayment()
        {
            try
            {
                var results = await _paymentIRepository.GetPaymentAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpGet]
        [Route("{PaymentId:Guid}")]
        public async Task<IActionResult> FetchPayment([FromRoute] Guid PaymentId)
        {
            var type = await _paymentIRepository.GetPayment(PaymentId);
            return Ok(type);
        }

        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> PostType(Payment payment)
        {

            var result = await _paymentIRepository.AddPayment(payment);
            if (result.PaymentId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok(new { message = "Added Successfully" });

        }



        [HttpPut]
        [Route("EditPayment")]
        public async Task<IActionResult> PutType(Guid Id, Payment editedPayment)
        {
            editedPayment.PaymentId = Id;
            await _paymentIRepository.EditPayment(editedPayment);
            return Ok(new { message = "Payment edited successfully" });
        }

        [HttpDelete]
        [Route("DeletePayment/{id}")]
        public JsonResult DeletePayment(Guid id)
        {
            _paymentIRepository.DeletePayment(id);
            return new JsonResult("Payment was deleted successfully");
        }
    }
}
