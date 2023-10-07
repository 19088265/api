using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemC : ControllerBase
    {
        private readonly ItemIRepository _itemIRepository;

        public ItemC(ItemIRepository itemIRepository)
        {
            _itemIRepository = itemIRepository;
        }

        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem()
        {
            try
            {
                var results = await _itemIRepository.GetItemAsync();
                return Ok(results);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpGet]
        [Route("{ItemId:Guid}")]
        public async Task<IActionResult> FetchItem([FromRoute] Guid ItemId)
        {
            var type = await _itemIRepository.GetItem(ItemId);
            return Ok(type);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> PostType(Items item)
        {

            try
            {
                var result = await _itemIRepository.AddItem(item);
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Item already exists" });
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
        [Route("EditItem")]
        public async Task<IActionResult> PutType(Guid Id, Items editedItem)
        {
            editedItem.ItemId = Id;
            await _itemIRepository.EditItem(editedItem);
            return Ok(new { message = "Item edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            bool deletionResult = _itemIRepository.DeleteItem(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete Item. It may be referenced by existing invoices.");
            }
        }
    }
}
