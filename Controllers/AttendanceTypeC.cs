using Architecture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceTypeC : ControllerBase
    {
        private readonly AttendanceTypeIRepository _attendanceIRepository;

        public AttendanceTypeC(AttendanceTypeIRepository attendanceIRepository)
        {
            _attendanceIRepository = attendanceIRepository;
        }

        [HttpGet]
        [Route("{TypeId:Guid}")]
        public async Task<IActionResult> GetTypeX([FromRoute] Guid TypeId)
        {
            var Type = await _attendanceIRepository.GetAttendanceType(TypeId);
            return Ok(Type);
        }
    }
}
