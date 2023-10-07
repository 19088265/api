using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class ProgramRepository : ProgramIRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProgramRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Program[]> GetAllProgramsAsync()
        {
            IQueryable<Program> query = _appDbContext.Program;
            return await query.ToArrayAsync();
        }

        public async Task<Program> GetProgram(Guid Id)
        {
            IQueryable<Program> query = _appDbContext.Program;
            return await query.FirstOrDefaultAsync(x => x.ProgramId == Id);
        }

        //Add program
        public async Task<Program> AddProgram(Program newProgram)
        {
            _appDbContext.Program.Add(newProgram);
            await _appDbContext.SaveChangesAsync();
            return newProgram;
        }

        //Update program 
        public async Task<Program> EditProgram(Program editedProgram)
        {
            _appDbContext.Entry(editedProgram).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedProgram;
        }



        public bool DeleteProgram(Guid Id)
        {


            //bool result = false;
            var program = _appDbContext.Program.Find(Id);
            if (program == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any schedule referencing this program
            bool hasReferencedSchedule = _appDbContext.Schedule.Any(s => s.ProgramId == Id);

            if (hasReferencedSchedule)
            {
                // There are referenced schedule, so we cannot delete the program
                return false;
            }

            try
            {
                _appDbContext.Program.Remove(program);
                _appDbContext.SaveChanges();
                return true; // Deletion successful
            }
            catch (Exception)
            {
                // Handle any exception that may occur during deletion
                return false; // Deletion failed
            }
        }
    }
}
