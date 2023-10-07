using Microsoft.EntityFrameworkCore;
using Architecture.Models;

namespace Architecture.Models
{
    // TaskRepository.cs
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ToDo[]> GetTasks()
        {
            IQueryable<ToDo> query = _appDbContext.Tasks;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<ToDo> GetTaskById(Guid taskId)
        {
            IQueryable<ToDo> query = _appDbContext.Tasks.Where(c => c.TaskId == taskId);
            return await query.FirstOrDefaultAsync();
        }

        //Add student
        public async Task<ToDo> AddTask(Models.ToDo newTask)
        {
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;
            _appDbContext.Tasks.Add(newTask);
            await _appDbContext.SaveChangesAsync();
            return newTask;
        }

        //Update student
        public async Task<ToDo> UpdateTask(Models.ToDo editedTask)
        {
            _appDbContext.Entry(editedTask).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedTask;
        }

        //Delete student
        public bool DeleteTask(Guid ID)
        {
            bool result = false;
            var task = _appDbContext.Tasks.FirstOrDefault(p => p.TaskId == ID);
            if (task != null)
            {
                _appDbContext.Tasks.Remove(task);
                _appDbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }

}
