using System.ComponentModel.DataAnnotations;
namespace Architecture.Models
{
    

        public class Attendance
        {

            public Guid AttendanceId { get; internal set; } = Guid.NewGuid();
            public Guid AttendanceTypeId { get; set; }
            public Guid CafeteriaId { get; set; }

            public int AttendanceNumber { get; set; }

            //Foreign Key
            //public Cafeteria Cafeteria { get; set; }
            //public AttendanceType AttendanceType { get; set; }

        }
}
