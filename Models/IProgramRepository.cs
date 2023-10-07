namespace Architecture.Models
{
    public interface IProgramRepository
    {
        // Programs
        Task<Program[]> GetAllProgramsAsync();
        Task<Program> AddProgram(Program newProgram);
        Task<Program> EditProgram(Program editedProgram);
        bool DeleteProgram(Guid id);
    }
}
