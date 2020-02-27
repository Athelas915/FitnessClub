using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public class Customer : Person
    {
        public virtual ICollection<SessionEnrollment> SessionEnrollments { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual Person Person { get; set; }
    }
}
