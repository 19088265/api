using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramC : ControllerBase
    {
        private readonly ProgramIRepository _programRepository;

        public ProgramC(ProgramIRepository programRepository)
        {
            _programRepository = programRepository;
        }

        [HttpGet]
        [Route("GetProgram")]
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

        [HttpGet]
        [Route("{ProgramId:Guid}")]
        public async Task<IActionResult> FetchProgram([FromRoute] Guid ProgramId)
        {
            var program = await _programRepository.GetProgram(ProgramId);
            return Ok(program);
        }

        //Add a program to the database
        [HttpPost]
        [Route("AddProgram")]
        public async Task<IActionResult> Post(Architecture.Models.Program program)
        {
            try
            {
                var result = await _programRepository.AddProgram(program);
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Program name already exists" });
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

        //Edit program
        [HttpPut]
        [Route("EditProgram")]
        public async Task<IActionResult> Put(Guid id, Architecture.Models.Program editedProgram)
        {

            editedProgram.ProgramId = id;
            await _programRepository.EditProgram(editedProgram);
            return Ok(new { message = "Program edited successfully" });
        }

        //Delete a program

        [HttpDelete]
        [Route("DeleteProgram/{id}")]
        public IActionResult DeleteProgram(Guid id)
        {
            bool deletionResult = _programRepository.DeleteProgram(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete program. It may be referenced by existing schedules.");
            }
        }
    }
}
