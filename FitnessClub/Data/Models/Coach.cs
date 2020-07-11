using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Coach : Employee
    {
        public Coach() { }

        public Coach(Coach c) : base(c)
        {
            if (c.Sessions != null)
            {
                Sessions = new List<Session>(c.Sessions);
            }
            if (c.CoachRatings != null)
            {
                CoachRatings = new List<CoachRating>(c.CoachRatings);
            }
        }

        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
    }
}
