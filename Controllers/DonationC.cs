using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Architecture.Models;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationC : ControllerBase
    {
        private readonly DonationIRepository _donationIRepository;

        public DonationC(DonationIRepository donationIRepository)
        {
            _donationIRepository = donationIRepository;
        }

        [HttpGet]
        [Route("GetDonations")]
        public async Task<IActionResult> GetDonations()
        {
            try
            {
                var result = await _donationIRepository.GetDonationAsync();
                return Ok(result);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{DonationId:Guid}")]
        public async Task<IActionResult> FetchDonation([FromRoute] Guid DonationId)
        {
            var donate = await _donationIRepository.GetDonation(DonationId);
            return Ok(donate);
        }

        [HttpPost]
        [Route("AddDonation")]
        public async Task<IActionResult> PostType(Donation donation)
        {
            var result = await _donationIRepository.AddDonation(donation);
            if (result.DonationId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok(new { message = "Added Successfully" });
        }

        [HttpPut]
        [Route("EditDonation")]
        public async Task<IActionResult> Put(Guid Id, Donation editedDonate)
        {
            editedDonate.DonationId = Id;
            await _donationIRepository.EditDonation(editedDonate);
            return Ok(new { message = "Donation edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteDonation/{id}")]
        public JsonResult DeleteDonation(Guid id)
        {
            _donationIRepository.DeleteDonation(id);
            return new JsonResult("Donation deleted successfully");
        }
    }
}
