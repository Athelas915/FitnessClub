using System;

namespace FitnessClub.Data.Models
{
    public class Holiday : DataEntity
    {
        public int HolidayID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeID { get; set; }
    }
}
