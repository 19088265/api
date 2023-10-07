using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Mail;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeC : ControllerBase
    {

        static int OTPnumber;



        [HttpPost]
        [Route("SendOTP")]
        public IActionResult Email([FromBody] string email)
        {
            Random random = new Random();
            OTPnumber = random.Next(1000, 9999);

            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("Mosuwe23@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Donation Authorization";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Message: Enter the following OTP:" + OTPnumber.ToString();
                smtp.Port = 587;
                smtp.Host = "smtp.zoho.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Mosuwe23@gmail.com", "Mosuwe23()");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return Ok(new { message = "OTP sent successfully" });
            }

        }




    }
}
