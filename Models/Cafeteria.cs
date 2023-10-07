namespace Architecture.Models
{
    public class Cafeteria
    {
        public Guid CafeteriaId { get; internal set; } = Guid.NewGuid();
        public Guid CafeteriaTypeId { get; set; }
        public string MealDescription { get; set; }
        //Data annotations for date
        public System.DateTime CafeteriaDate { get; set; }

       // public ICollection<Attendance> Attendance { get; set; }

        //ForeignKey
        public CafeteriaType CafeteriaType { get; set; }
    }
}
