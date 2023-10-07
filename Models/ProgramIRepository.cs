namespace Architecture.Models
{
    public interface ProgramIRepository
    {
        // Programs
        Task<Program[]> GetAllProgramsAsync();

        Task<Program> GetProgram(Guid ProgramId);
        Task<Program> AddProgram(Program newProgram);
        Task<Program> EditProgram(Program editedProgram);
        bool DeleteProgram(Guid id);
    }
}
