using System;

namespace FC.Data.Models
{
    class Holiday : BaseEntity
    {
        public int HolidayID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
