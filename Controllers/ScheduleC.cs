using Architecture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace RoseApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleC : ControllerBase
    {
        private readonly ScheduleIRepository _scheduleIRepository;

        public ScheduleC(ScheduleIRepository scheduleIRepository)
        {
            _scheduleIRepository = scheduleIRepository;
        }

        [HttpGet]
        [Route("GetSchedule")]
        public async Task<IActionResult> GetAllSchedule()
        {
            try
            {
                var results = await _scheduleIRepository.GetScheduleAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{ScheduleId:Guid}")]
        public async Task<IActionResult> FetchSchedule([FromRoute] Guid ScheduleId)
        {
            var sched = await _scheduleIRepository.GetSchedule(ScheduleId);
            return Ok(sched);
        }

        //Add a program to the database
        [HttpPost]
        [Route("AddSchedule")]
        public async Task<IActionResult> Post(Schedule sched)
        {

            var result = await _scheduleIRepository.AddSchedule(sched);
            if (result.ScheduleId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok(new { message = "Added Successfully" });
        }

        //Edit schedule
        [HttpPut]
        [Route("EditSchedule")]
        public async Task<IActionResult> Put(Guid id, Schedule editedSchedule)
        {


            editedSchedule.ScheduleId = id;
            await _scheduleIRepository.EditSchedule(editedSchedule);
            return Ok(new { message = "Edited Successfully" });
        }

        //Delete schedule
        [HttpDelete]
        [Route("DeleteSchedule/{id}")]
        public JsonResult DeleteSchedule(Guid id)
        {
            _scheduleIRepository.DeleteSchedule(id);
            return new JsonResult("Schedule was deleted successfully");
        }
    }
}
