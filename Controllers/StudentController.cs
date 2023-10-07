using Architecture.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers
{
    //[Authorize(Policy = "ProgramRights")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("SearchStudent")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudents(string query)
        {
            var students = await _studentRepository.SearchStudentsAsync(query);
            if (students == null || students.Length == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }

        //Get all students
        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                var results = await _studentRepository.GetAllStudentAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetStudent/{studentId}")]
        public async Task<IActionResult> GetStudentAsync(Guid studentId)
        {
            try
            {
                var result = await _studentRepository.GetOneStudentAsync(studentId);

                if (result == null) return NotFound("Student does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add student to the database
        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> Post(Student student)
        {
            // Get the existing student type by ID
            var existingStudentType = await _studentRepository.GetOneStudentTypeAsync((Guid)student.StudentTypeId);

            if (existingStudentType == null)
            {
                return BadRequest("Invalid student type ID"); // Return a 400 Bad Request if the student type ID doesn't exist
            }

            // Set the employee's type to the existing one
            student.StudentType = existingStudentType;

            var result = await _studentRepository.AddStudent(student);
            if (result == null || result.StudentId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Student added successfully");
        }

        //Edit student
        [HttpPut]
        [Route("EditStudent")]
        public async Task<IActionResult> Put(Guid id, Models.Student editedStudent)
        {
            editedStudent.StudentId = id;
            await _studentRepository.EditStudent(editedStudent);
            return Ok("Student edited successfully");
        }

        //Delete student
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public JsonResult DeleteStudent(Guid id)
        {
            _studentRepository.DeleteStudent(id);
            return new JsonResult("Student deleted successfully");
        }




        /////////////////////////////////////////////////////////////////////
        //View student types
        [HttpGet]
        [Route("GetAllStudentType")]
        public async Task<IActionResult> GetAllStudentType()
        {
            try
            {
                var results = await _studentRepository.GetStudentTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetStudentType/{studentTypeId}")]
        public async Task<IActionResult> GetOneStudentTypeAsync(Guid studentTypeId)
        {
            try
            {
                var result = await _studentRepository.GetOneStudentTypeAsync(studentTypeId);

                if (result == null) return NotFound("Student type does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add a student type to the database
        [HttpPost]
        [Route("AddStudentType")]
        public async Task<IActionResult> Post(StudentType studentType)
        {
            var result = await _studentRepository.AddStudentType(studentType);
            if (result == null || result.StudentTypeId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Student added successfully");
        }

        //Edit student type
        [HttpPut]
        [Route("EditStudentType")]
        public async Task<IActionResult> Put(Guid id, Models.StudentType editedStudentType)
        {
            editedStudentType.StudentTypeId = id;
            await _studentRepository.EditStudentType(editedStudentType);
            return Ok("Student type edited successfully");
        }

        //Delete student type
        [HttpDelete]
        [Route("DeleteStudentType/{ID}")]
        public IActionResult DeleteStudentType(Guid ID)
        {
            try
            {
                bool isDeleted = _studentRepository.DeleteStudentType(ID);

                if (isDeleted)
                {
                    return Ok(new { message = "Student type deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Student type not found or referenced by student, deletion failed" });
                }
            }
            catch (Exception)
            {
                // Log the exception for debugging purposes.
                // You can also customize the error message as needed.
                return StatusCode(500, new { message = "An error occurred while deleting the student type" });
            }
        }
    }
}
