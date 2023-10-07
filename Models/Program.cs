namespace Architecture.Models
{
    public class Program
    {
        public Guid ProgramId { get; internal set; } = Guid.NewGuid();
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
    }
}
