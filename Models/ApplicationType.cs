namespace Architecture.Models
{
    public class ApplicationType
    {
        public Guid? ApplicationTypeId { get; internal set; } = Guid.NewGuid();
        public string ApplicationTypeDescription { get; set; }
    }
}
