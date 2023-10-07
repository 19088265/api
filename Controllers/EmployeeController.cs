using Architecture.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers
{
    //[Authorize(Policy = "ProgramRights")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //[Authorize(Policy = "ProgramRights")]
        [HttpGet("GetWeatherForecastForAdminsOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetForAdminOnly()
        {
            return Ok("Yeah, we have some secret weather only for admins.");
        }

        //Employee  functions
        [HttpGet]
        [Route("SearchEmployee")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string query)
        {
            var employees = await _employeeRepository.SearchEmployeesAsync(query);
            if (employees == null || employees.Length == 0)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var results = await _employeeRepository.GetAllEmployeeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetEmployee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid employeeId)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeAsync(employeeId);

                if (result == null) return NotFound("Employee does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add employee to the database
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee employee)
        {
            // Get the existing employee type by ID
            var existingEmployeeType = await _employeeRepository.GetOneEmployeeTypeAsync((Guid)employee.EmployeeTypeId);

            if (existingEmployeeType == null)
            {
                return BadRequest("Invalid employee type ID"); // Return a 400 Bad Request if the employee type ID doesn't exist
            }

            // Set the employee's type to the existing one
            employee.EmployeeType = existingEmployeeType;

            var result = await _employeeRepository.AddEmployee(employee);
            if (result == null || result.EmployeeId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Added successfully");
        }

        //Edit Employee
        [HttpPut]
        [Route("EditEmployee")]
        public async Task<IActionResult> Put(Guid id, Models.Employee editedEmployee)
        {
            editedEmployee.EmployeeId = id;
            await _employeeRepository.EditEmployee(editedEmployee);
            return Ok("Employee edited successfully");
        }

        //Delete Employee
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public JsonResult DeleteEmployee(Guid id)
        {
            _employeeRepository.DeleteEmployee(id);
            return new JsonResult("Employee deleted successfully");
        }




        /////////////////////////////////////////////////////////////////////
        //Enployee type functions
        [HttpGet]
        [Route("GetAllEmployeeType")]
        public async Task<IActionResult> GetAllEmployeeType()
        {
            try
            {
                var results = await _employeeRepository.GetEmployeeTypesAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetEmployeeType/{employeeTypeId}")]
        public async Task<IActionResult> GetOneEmployeeTypeAsync(Guid employeeTypeId)
        {
            try
            {
                var result = await _employeeRepository.GetOneEmployeeTypeAsync(employeeTypeId);

                if (result == null) return NotFound("Employee type does not exist. You need to create it first");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        //Add a employee type to the database
        [HttpPost]
        [Route("AddEmployeeType")]
        public async Task<IActionResult> Post(EmployeeType employeeType)
        {
            var result = await _employeeRepository.AddEmployeeType(employeeType);
            if (result == null || result.EmployeeTypeId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Added successfully");
        }

        //Edit employee Type
        [HttpPut]
        [Route("EditEmployeeType")]
        public async Task<IActionResult> Put(Guid id, Models.EmployeeType editedEmployeeType)
        {
            editedEmployeeType.EmployeeTypeId = id;
            await _employeeRepository.EditEmployeeType(editedEmployeeType);
            return Ok("Employee type edited successfully");
        }

        //Delete employee type
        [HttpDelete]
        [Route("DeleteEmployeeType/{ID}")]
        public IActionResult DeleteEmployeeType(Guid ID)
        {
            try
            {
                bool isDeleted = _employeeRepository.DeleteEmployeeType(ID);

                if (isDeleted)
                {
                    return Ok(new { message = "Employee type deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Employee type not found or referenced by employee, deletion failed" });
                }
            }
            catch (Exception)
            {
                // Log the exception for debugging purposes.
                // You can also customize the error message as needed.
                return StatusCode(500, new { message = "An error occurred while deleting the employee type" });
            }
        }
    }
}
