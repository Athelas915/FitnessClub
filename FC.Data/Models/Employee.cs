using System.Collections.Generic;

namespace FC.Data.Models
{
    class Employee : Person
    {
        public int EmployeeID { get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
