using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Architecture.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static QRCoder.PayloadGenerator;
using PhoneNumber = Twilio.Types.PhoneNumber;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorC : ControllerBase
    {
        private readonly SponsorIRepository _sponsorIRepository;

        public SponsorC(SponsorIRepository sponsorIRepository)
        {
            _sponsorIRepository = sponsorIRepository;
        }



        [HttpGet]
        [Route("GetSponsors")]
        public async Task<IActionResult> GetSponsors()
        {
            try
            {
                var results = await _sponsorIRepository.GetSponsorsAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{SponsorId:Guid}")]
        public async Task<IActionResult> FetchSponsor([FromRoute] Guid SponsorId)
        {
            var sponsor = await _sponsorIRepository.GetSponsor(SponsorId);
            return Ok(sponsor);
        }

        [HttpPost("sendSMS")]
        public IActionResult sendSMS(string number, string messages)
        {


            //  TwilioClient.Init(accountSid, authToken);

            // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            var accountSid = "AC113e81db3a477c78fb3ea8388c07babd";
            var authToken = "68ce8493aeadeb56bd45c70c6b9cc9a9";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(number));
            messageOptions.From = new PhoneNumber("+1 618 310 3384");
            //messageOptions.MessagingServiceSid = null;
            messageOptions.Body = messages;

            var message = MessageResource.Create(messageOptions);



            return Ok(1);
        }







        [HttpPost]
        [Route("AddSponsor")]
        public async Task<IActionResult> Post(Sponsor sponsor)
        {
            var result = await _sponsorIRepository.AddSponsor(sponsor);
            if (result.SponsorId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            else
            {
                sendSMS(sponsor.SponsorContactNumber, "Dear Sponsor,You Have been added to the Little Rose system check your email for furthur instructions");
                return Ok(new { message = "Added Successfully" });
            }

        }



        [HttpPut]
        [Route("EditSponsor")]
        public async Task<IActionResult> Put(Guid Id, Sponsor editedSponsor)
        {
            editedSponsor.SponsorId = Id;
            await _sponsorIRepository.EditSponsor(editedSponsor);
            return Ok(new { message = "Sponsor edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteSponsor/{id}")]
        public IActionResult DeleteSponsor(Guid id)
        {
            bool deletionResult = _sponsorIRepository.DeleteSponsor(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete Sponsor. It may be referenced by existing donations.");
            }
            //_sponsorIRepository.DeleteSponsor(id);
            //return new JsonResult("Sponsor deleted successfully");
        }



        ///InventoryType///

        [HttpGet]
        [Route("GetSponsorTypes")]
        public async Task<IActionResult> GetSponsorType()
        {
            try
            {
                var results = await _sponsorIRepository.GetSponsorTypeAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }







        [HttpPost]
        [Route("AddSponsorType")]
        public async Task<IActionResult> PostType(SponsorType sponsorType)
        {

            try
            {
                var result = await _sponsorIRepository.AddSponsorType(sponsorType);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Sponsor type already exists" });
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
        [Route("EditSponsorType")]
        public async Task<IActionResult> PutType(Guid Id, SponsorType editedSponsorType)
        {
            editedSponsorType.SponsorTypeId = Id;
            await _sponsorIRepository.EditSponsorType(editedSponsorType);
            return Ok(new { message = "SponsorType edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteSponsorType/{id}")]
        public IActionResult DeleteSponsorType(Guid id)
        {
            bool deletionResult = _sponsorIRepository.DeleteSponsorType(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete SponsorType. It may be referenced by existing sponsors.");
            }
        }

    }
}
