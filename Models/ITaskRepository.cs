namespace Architecture.Models
{
    public interface ITaskRepository
    {
        Task<ToDo[]> GetTasks();
        Task<ToDo> GetTaskById(Guid taskId);
        Task<ToDo> AddTask(ToDo newTask);
        Task<ToDo> UpdateTask(ToDo editedTask);
        bool DeleteTask(Guid ID);
    }
}
