using System;

namespace FitnessClub.Data.Models
{
    public class Holiday : BaseEntity
    {
        public int HolidayID { get; set; }
        public int PersonID { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
