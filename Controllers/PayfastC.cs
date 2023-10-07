using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Architecture.Models;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayfastC : ControllerBase
    {
        private readonly AppDbContext _context;

        public PayfastC(AppDbContext roseDbContext)
        {
            _context = roseDbContext;
        }

        private const string PayfastMerchantKey = "v8q3gl3og5ap4";

        [Route("payfast")]
        [HttpPost]
        public IActionResult ReceiveItn([FromForm] IFormCollection form)
        {
            // Step 1: Parse the ITN request parameters


            // Step 2: Validate the signature
            if (!IsValidPayfastSignature(form))
            {
                return BadRequest("Invalid Payfast signature");
            }
            //Cannot implicity convert type 'Microsoft.AspNetCore.Http.IformCollection' to 'System.Collections.Specialized.NameValueCollection'. An explicit conversion exists (are you missing a cast?)

            // Step 3: Process the payment confirmation
            string paymentStatus = form["payment_status"];
            string mPaymentId = form["m_payment_id"];

            // Perform actions based on payment status (e.g., update your database)
            if (paymentStatus == "COMPLETE")
            {
                UpdateInvoiceStatus(mPaymentId, "Paid");
                // Payment was successful
                // Update your database or perform other actions here
            }
            else if (paymentStatus == "FAILED")
            {
                // Payment failed
                // Handle payment failure here
                UpdateInvoiceStatus(mPaymentId, "Payment Outstanding");
            }

            // Return a response to Payfast to acknowledge receipt of the ITN
            return Ok("ITN received and processed successfully");
        }

        private bool IsValidPayfastSignature(IFormCollection form)
        {
            string receivedSignature = form["signature"];
            string expectedSignature = GenerateExpectedPayfastSignature(form);

            return receivedSignature == expectedSignature;
        }

        private string GenerateExpectedPayfastSignature(IFormCollection form)
        {
            // Create a signature string using the received parameters
            // Make sure to use the same order of parameters as used in your payment request

            string signatureString = $"merchant_id={form["merchant_id"]}&merchant_key={PayfastMerchantKey}&" +
                $"return_url={form["return_url"]}&cancel_url={form["cancel_url"]}&" +
                $"notify_url={form["notify_url"]}&m_payment_id={form["m_payment_id"]}&" +
                $"amount={form["amount"]}&item_name={form["item_name"]}";

            // Generate the HMAC-SHA256 signature using your Payfast merchant key
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(PayfastMerchantKey)))
            {
                byte[] data = Encoding.UTF8.GetBytes(signatureString);
                byte[] hash = hmac.ComputeHash(data);

                // Convert the hash to a hexadecimal string
                string expectedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();

                return expectedSignature;
            }
        }

        private void UpdateInvoiceStatus(string invoiceId, string status)
        {
            // Update the IsPaid column in your database for the corresponding invoice
            // Replace this with your actual database update logic

            var invoice = _context.Invoice.FirstOrDefault(i => i.InvoiceId == new Guid(invoiceId));

            if (invoice != null)
            {
                invoice.IsPaid = status;
                _context.SaveChanges();
            }
        }









    }
}
