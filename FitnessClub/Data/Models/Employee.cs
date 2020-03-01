using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Employee : Person
    {
        public int EmployeeID { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual Person Person { get; set; }
    }
}
