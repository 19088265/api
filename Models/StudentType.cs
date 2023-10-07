namespace Architecture.Models
{
    public class StudentType
    {
        public Guid? StudentTypeId { get; internal set; } = Guid.NewGuid();
        public string StudentTypeDescription { get; set; }
    }
}
