using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Employee : Person
    {
        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
