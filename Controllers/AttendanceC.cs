using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Architecture.Models;
using Architecture.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceC : ControllerBase
    {
        private readonly AttendanceIRepository _attendanceIRepository;

        public AttendanceC(AttendanceIRepository attendanceIRepository)
        {
            _attendanceIRepository = attendanceIRepository;
        }

        // [HttpGet]
        //[Route("{CafeId:guid}/Beneficiaries")]
        //public async Task<IActionResult> GetBeneficiariesBySessionId([FromRoute] Guid CafeId)
        //{
        //  try
        //{
        //  var beneficiaries = await _attendanceIRepository.GetBeneficiariesByCafeteriaId(CafeId);
        //return Ok(beneficiaries);
        //}
        //catch (Exception)
        //{
        //  return StatusCode(500, "Internal Server Error. Please contact support.");
        //}
        //}

        [HttpPost]
        [Route("SaveAttendanceX")]
        public async Task<IActionResult> SaveAttendance([FromRoute] Attendance[] attendanceData)
        {
            try
            {
                var result = await _attendanceIRepository.SaveAttendance(attendanceData);
                if (result)
                {
                    return Ok("Attendance saved successfully");
                }
                else
                {
                    return StatusCode(500, "Failed to save attendance");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("SaveAttendance")]
        public async Task<IActionResult> SaveAttendance([FromBody] AttendanceVM attendanceData)
        {
            try
            {

                // Create individual attendance records for each selected beneficiary


                Attendance attendance = new Attendance
                {
                    AttendanceId = attendanceData.AttendanceId,
                    CafeteriaId = attendanceData.CafeteriaId,
                    //BeneficiaryId = data.BeneficiaryId,
                    AttendanceNumber = attendanceData.AttendanceNumber
                };

                // Save the attendance entity to the database
                await _attendanceIRepository.AddAttendance(attendance);


                return Ok("Attendance data saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving attendance data: {ex.Message}");
            }



        }

        [HttpGet]
        [Route("GetAttendance")]
        public async Task<IActionResult> GetAttendance()
        {
            try
            {
                var results = await _attendanceIRepository.GetAttendanceAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("{AttendanceId:Guid}")]
        public async Task<IActionResult> FetchAttendance([FromRoute] Guid AttendanceId)
        {
            var attend = await _attendanceIRepository.GetAttendance(AttendanceId);
            return Ok(attend);
        }

        [HttpPost]
        [Route("TakeAttendance")]
        public async Task<IActionResult> Post(Attendance attendances)
        {

            // Save the attendance record to the database

            try
            {
                var result = await _attendanceIRepository.TakeAttendance(attendances);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Attendance for this cafeteria session already exists" });
                }
                else
                {
                    // Handle other database-related errors
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }


        //AttendanceType
        [HttpGet]
        [Route("GetAttendanceType")]
        public async Task<IActionResult> GetAttendanceType()
        {
            try
            {
                var results = await _attendanceIRepository.GetAttendanceTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddAttendanceType")]
        public async Task<IActionResult> PostType(AttendanceType type)
        {

            try
            {
                var result = await _attendanceIRepository.AddAttendanceType(type);
                return Ok(new { message = "Added Successfully" });
            }
            catch (Exception ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    // Return a conflict response indicating the duplicate province name
                    return Conflict(new { message = "Attendance type already exists" });
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
        [Route("EditAttendanceType")]
        public async Task<IActionResult> PutType(Guid Id, AttendanceType editedAttendanceType)
        {
            editedAttendanceType.AttendanceTypeId = Id;
            await _attendanceIRepository.EditAttendanceType(editedAttendanceType);
            return Ok(new { message = "AttendanceType edited successfully" });
        }

        [HttpDelete]
        [Route("DeleteAttendanceType/{id}")]
        public IActionResult DeleteAttendanceType(Guid id)
        {
            bool deletionResult = _attendanceIRepository.DeleteAttendanceType(id);

            if (deletionResult)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Unable to delete AttendanceType. It may be referenced by existing attendanceType.");
            }
        }
    }
}
