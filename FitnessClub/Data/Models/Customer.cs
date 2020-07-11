using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Customer : Person
    {
        public Customer() { }

        public Customer(Customer c) : base(c)
        {
            if (c.SessionEnrollments != null)
            {
                SessionEnrollments = new List<SessionEnrollment>(c.SessionEnrollments);
            }
            if (c.Memberships != null)
            {
                Memberships = new List<Membership>(c.Memberships);
            }
            if (c.CoachRatings != null)
            {
                CoachRatings = new List<CoachRating>(c.CoachRatings);
            }
        }

        public virtual ICollection<SessionEnrollment> SessionEnrollments { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
    }
}
