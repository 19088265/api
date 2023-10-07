using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceC : ControllerBase
    {
        private readonly InvoiceIRepository _invoiceIRepository;

        public InvoiceC(InvoiceIRepository invoiceIRepository)
        {
            _invoiceIRepository = invoiceIRepository;
        }

        [HttpGet]
        [Route("GetInvoice")]
        public async Task<IActionResult> GetInvoice()
        {
            try
            {
                var results = await _invoiceIRepository.GetInvoiceAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpGet]
        [Route("{InvoiceId:Guid}")]
        public async Task<IActionResult> FetchInvoice([FromRoute] Guid InvoiceId)
        {
            var type = await _invoiceIRepository.GetInvoice(InvoiceId);
            return Ok(type);
        }

        [HttpPost]
        [Route("AddInvoice")]
        public async Task<IActionResult> PostType(Invoice invoice)
        {
            var result = await _invoiceIRepository.AddInvoice(invoice);
            if (result.InvoiceId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok(new { message = "Added Successfully" });


        }



        [HttpPut]
        [Route("EditInvoice")]
        public async Task<IActionResult> PutType(Guid Id, Invoice editedInvoice)
        {
            editedInvoice.InvoiceId = Id;
            await _invoiceIRepository.EditInvoice(editedInvoice);
            return Ok(new { message = "Invoice edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteInvoice/{id}")]
        public JsonResult DeleteInvoice(Guid id)
        {
            _invoiceIRepository.DeleteInvoice(id);
            return new JsonResult("Invoice deleted successfully");
        }

        ////////////////////////////// Invoice items //////////////////////////////////////
        [HttpGet("GetInvoiceItems/{invoiceId}")]
        public async Task<ActionResult<IEnumerable<ShopItem>>> GetInvoiceItems(Guid invoiceId)
        {
            var items = await _invoiceIRepository.GetInvoiceItems(invoiceId);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // POST api/shopitems/{invoiceId}
        [HttpPost("AddInvoiceItem/{invoiceId}")]
        public async Task<ActionResult<ShopItem>> AddInvoiceItem(Guid invoiceId, ShopItem newShopItem)
        {
            var addedItem = await _invoiceIRepository.AddInvoiceItem(invoiceId, newShopItem);

            if (addedItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetInvoiceItems", new { invoiceId = invoiceId }, addedItem);
        }

        // PUT api/shopitems/{invoiceId}/{shopItemId}
        [HttpPut("EditInvoiceItem/{invoiceId}/{shopItemId}")]
        public async Task<IActionResult> EditInvoiceItem(Guid invoiceId, Guid shopItemId, ShopItem updatedShopItem)
        {
            if (shopItemId != updatedShopItem.ShopItemId || invoiceId != updatedShopItem.InvoiceId)
            {
                return BadRequest();
            }

            var result = await _invoiceIRepository.EditInvoiceItem(updatedShopItem);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/shopitems/{shopItemId}
        [HttpDelete("DeleteInvoiceItem{shopItemId}")]
        public async Task<IActionResult> DeleteInvoiceItem(Guid shopItemId)
        {
            var result = _invoiceIRepository.DeleteInvoiceItem(shopItemId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
