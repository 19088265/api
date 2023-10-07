namespace Architecture.Models
{
    public class ApplicationStatus
    {
        public Guid? ApplicationStatusId { get; internal set; } = Guid.NewGuid();
        public string ApplicationStatusDescription { get; set; }
    }
}
