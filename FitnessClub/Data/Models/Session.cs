using System;
using System.Collections.Generic;

namespace FitnessClub.Data.Models
{
    public enum SessionType
    {
        aerobics, spinning, boxing, yoga, pilates
    }
    public class Session : BaseEntity
    {
        public int SessionID { get; set; }
        public int PersonID { get; set; }
        public virtual Coach Coach { get; set; }
        public SessionType? SessionType { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int Room { get; set; }
        public virtual ICollection<SessionEnrollment> SessionEnrollments { get; set; }
    }
}
