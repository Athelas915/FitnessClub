using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Coach : Employee
    {
        public virtual Employee Employee { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
    }
}
