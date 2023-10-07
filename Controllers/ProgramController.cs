using Architecture.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramRepository _programRepository;

        public ProgramController(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }

        [HttpGet]
        [Route("GetAllPrograms")]
        public async Task<IActionResult> GetAllPrograms()
        {
            try
            {
                var results = await _programRepository.GetAllProgramsAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Add a program to the database
        [HttpPost]
        [Route("AddProgram")]
        public async Task<IActionResult> Post(Models.Program program)
        {
            var result = await _programRepository.AddProgram(program);
            if (result.ProgramId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Program was added successfully");
        }

        //Edit program
        [HttpPut]
        [Route("EditProgram")]
        public async Task<IActionResult> Put(Guid id, Models.Program editedProgram)
        {
            // _programRepository.EditProgram(editedProgram);
            //return Ok("Program was edited successfully");

            editedProgram.ProgramId = id;
            await _programRepository.EditProgram(editedProgram);
            return Ok("Program was edited successfully");
        }

        //Delete a program
        [HttpDelete]
        [Route("DeleteProgram/{id}")]
        public JsonResult DeleteProgram(Guid id)
        {
            _programRepository.DeleteProgram(id);
            return new JsonResult("Program was deleted successfully");
        }
    }
}
