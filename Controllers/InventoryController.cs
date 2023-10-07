using Architecture.Dtos;
using Architecture.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        [Route("GetAllInventory")]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var results = await _inventoryRepository.GetAllInventoryAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetInventory/{inventoryId}")]
        public async Task<IActionResult> GetInvetoryAsync(Guid inventoryId)
        {
            try
            {
                var result = await _inventoryRepository.GetInventoryAsync(inventoryId);

                if (result == null) return NotFound("Inventory does not exist. You need to add it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add inventory to the database
        [HttpPost]
        [Route("AddInventory")]
        public async Task<IActionResult> Post(Inventory inventory)
        {
            // Get the existing employee type by ID
            var existingInventoryType = await _inventoryRepository.GetInventoryTypeAsync(inventory.InventoryTypeId);

            if (existingInventoryType == null)
            {
                return BadRequest("Invalid employee type ID"); // Return a 400 Bad Request if the employee type ID doesn't exist
            }

            // Set the employee's type to the existing one
            inventory.InventoryType = existingInventoryType;

            var result = await _inventoryRepository.AddInventory(inventory);
            if (result.InventoryId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Added successfully");
        }

        //Edit Inventory
        [HttpPut]
        [Route("EditInventory")]
        public async Task<IActionResult> Put(Guid id, Models.Inventory editedInventory)
        {
            editedInventory.InventoryId = id;
            await _inventoryRepository.EditInventory(editedInventory);
            return Ok("Inventory edited successfully");
        }

        //Delete inventory
        [HttpDelete]
        [Route("DeleteInventory/{id}")]
        public JsonResult DeleteInventory(Guid id)
        {
            _inventoryRepository.DeleteInventory(id);
            return new JsonResult("Inventory deleted successfully");
        }



        //////////////////////////////////////////////////////////////////////////////////////////////
        //Inventory type functions
        [HttpGet]
        [Route("GetAllInventoryType")]
        public async Task<IActionResult> GetAllInventoryType()
        {
            try
            {
                var results = await _inventoryRepository.GetInventoryTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetInventoryType/{inventoryTypeId}")]
        public async Task<IActionResult> GetInventoryTypeAsync(Guid inventoryTypeId)
        {
            try
            {
                var result = await _inventoryRepository.GetInventoryTypeAsync(inventoryTypeId);

                if (result == null) return NotFound("Inventory type does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add a inventory type to the database
        [HttpPost]
        [Route("AddInventoryType")]
        public async Task<IActionResult> Post(InventoryType inventoryType)
        {
            var result = await _inventoryRepository.AddInventoryType(inventoryType);
            if (result.InventoryTypeId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Added successfully");
        }

        //Edit Inventory Type
        [HttpPut]
        [Route("EditInventoryType")]
        public async Task<IActionResult> Put(Guid id, Models.InventoryType editedInventoryType)
        {
            editedInventoryType.InventoryTypeId = id;
            await _inventoryRepository.EditInventoryType(editedInventoryType);
            return Ok("Inventory type edited successfully");
        }

        //Delete inventory type
        [HttpDelete]
        [Route("DeleteInventoryType/{id}")]
        public IActionResult DeleteInventoryType(Guid ID)
        {
            try
            {
                bool isDeleted = _inventoryRepository.DeleteInventoryType(ID);

                if (isDeleted)
                {
                    return Ok(new { message = "Inventory type deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Inventory type not found or referenced by inventory, deletion failed" });
                }
            }
            catch (Exception)
            {
                // Log the exception for debugging purposes.
                // You can also customize the error message as needed.
                return StatusCode(500, new { message = "An error occurred while deleting the inventory type" });
            }
        }



        /////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("GetQuantitiesPerType")]
        public async Task<ActionResult<IEnumerable<InventoryTypeQuantityDto>>> GetInventoryQuantitiesPerType()
        {
            var inventoryQuantities = await _inventoryRepository.GetInventoryQuantitiesPerTypeAsync();
            return Ok(inventoryQuantities);
        }
    }
}
