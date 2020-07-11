using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Employee : Person
    {
        public Employee() { }

        public Employee(Employee e) : base(e)
        {
            if (e.Holidays != null)
            {
                Holidays = new List<Holiday>(e.Holidays);
            }
        }

        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
