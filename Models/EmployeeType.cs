namespace Architecture.Models
{
    public class EmployeeType
    {
        public Guid? EmployeeTypeId { get; internal set; } = Guid.NewGuid();
        public string EmployeeTypeDescription { get; set; }
    }
}
