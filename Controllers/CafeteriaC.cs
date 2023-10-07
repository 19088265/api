using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Architecture.Models;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeteriaC : ControllerBase
    {
        private readonly CafeteriaIRepository _cafeteriaIRepository;

        public CafeteriaC(CafeteriaIRepository cafeteriaIRepository)
        {
            _cafeteriaIRepository = cafeteriaIRepository;
        }

        [HttpGet]
        [Route("GetCafeterias")]
        public async Task<IActionResult> GetCafeterias()
        {
            try
            {
                var result = await _cafeteriaIRepository.GetCafeteriaAsync();
                return Ok(result);


            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{CafeId:Guid}")]
        public async Task<IActionResult> FetchCafeteria([FromRoute] Guid CafeId)
        {
            var cafe = await _cafeteriaIRepository.GetCafeteria(CafeId);
            return Ok(cafe);
        }

        [HttpPost]
        [Route("AddCafeteria")]
        public async Task<IActionResult> Post(Cafeteria cafe)
        {
            var result = await _cafeteriaIRepository.AddCafeteria(cafe);
            if (result.CafeteriaId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok(new { message = "Added Successfully" });
        }

        [HttpPut]
        [Route("EditCafeteria")]
        public async Task<IActionResult> Put(Guid Id, Cafeteria editedCafe)
        {
            editedCafe.CafeteriaId = Id;
            await _cafeteriaIRepository.EditCafeteria(editedCafe);
            return Ok(new { message = "Cafeteria edited successfully" });
        }


        [HttpDelete]
        [Route("DeleteCafeteria/{id}")]
        public IActionResult DeleteCafeteriaType(Guid id)
        {
            bool deletionResult = _cafeteriaIRepository.DeleteCafeteria(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete Cafeteria. It may be referenced by existing attendances.");
            }
        }


    }
}
