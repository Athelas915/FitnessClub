using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Coach : Employee
    {
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
    }
}
