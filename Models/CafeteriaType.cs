namespace Architecture.Models
{
    public class CafeteriaType
    {
        public Guid CafeteriaTypeId { get; internal set; } = Guid.NewGuid();
        public string CafeteriaTypeDescription { get; set; }

        //public List<Cafeteria> Cafeterias { get; set; }
    }
}
