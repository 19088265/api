using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Architecture.Models;

namespace Architecture.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CafeteriaAttendanceReportC : ControllerBase
    {
        private readonly AppDbContext _context;

        public CafeteriaAttendanceReportC(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCafeteriaAttendanceData")]
        public IActionResult GetCafeteriaAttendanceData(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Retrieve cafeteria sessions with the specified cafeteria types
                var cafeteriaSessions = _context.Cafeteria
                    .Include(cs => cs.CafeteriaType)
                    .Where(cs => cs.CafeteriaType.CafeteriaTypeDescription == "Breakfast" ||
                                 cs.CafeteriaType.CafeteriaTypeDescription == "Lunch")
                    .Where(cs => cs.CafeteriaDate >= startDate && cs.CafeteriaDate <= endDate)
                    .ToList();

                // Retrieve attendance data for the selected cafeteria sessions
                var attendanceData = cafeteriaSessions.Select(cs => new
                {
                    CafeteriaTypeDescription = cs.CafeteriaType.CafeteriaTypeDescription,
                    CafeteriaDate = cs.CafeteriaDate,
                    AttendanceNumber = _context.Attendance
                        .Where(a => a.CafeteriaId == cs.CafeteriaId)
                        .Sum(a => a.AttendanceNumber)
                }).ToList();

                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving cafeteria attendance data.");
            }
        }
    }
}
