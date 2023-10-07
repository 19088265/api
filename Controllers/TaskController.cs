using Architecture.Models;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //Get all students
        [HttpGet]
        [Route("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var results = await _taskRepository.GetTasks();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //Get one record
        [HttpGet]
        [Route("GetTask/{taskId}")]
        public async Task<IActionResult> GetTask(Guid taskId)
        {
            try
            {
                var result = await _taskRepository.GetTaskById(taskId);

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
        [Route("AddTask")]
        public async Task<IActionResult> AddTask(ToDo task)
        {

            var result = await _taskRepository.AddTask(task);
            if (result == null || result.TaskId == Guid.Empty)
            {
                return StatusCode(500, "Internal server Error. Please contact support");
            }
            return Ok("Student added successfully");
        }

        //Edit student
        [HttpPut]
        [Route("EditTask")]
        public async Task<IActionResult> UpdateTask(Guid id, ToDo editedTask)
        {
            editedTask.TaskId = id;
            _taskRepository.UpdateTask(editedTask);
            return Ok("Student edited successfully");
        }

        //Delete student
        [HttpDelete]
        [Route("DeleteTask/{id}")]
        public JsonResult DeleteTask(Guid id)
        {
            _taskRepository.DeleteTask(id);
            return new JsonResult("Student deleted successfully");
        }
    }
}
