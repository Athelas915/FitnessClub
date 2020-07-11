using System;

namespace FitnessClub.Data.Models
{
    public class Holiday : DataEntity
    {
        public Holiday() { }

        public Holiday(Holiday h)
        {
            CreatedOn = h.CreatedOn;
            CreatedBy = h.CreatedBy;
            HolidayID = h.HolidayID;
            Start = h.Start;
            Finish = h.Finish;
            EmployeeID = h.EmployeeID;
            if (h.Employee != null)
            {
                Employee = new Employee(h.Employee);
            }
        }

        public int HolidayID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeID { get; set; }
    }
}
