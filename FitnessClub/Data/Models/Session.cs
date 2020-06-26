using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitnessClub.Data.Models
{
    public enum SessionType
    {
        Aerobics, Spinning, Boxing, Yoga, Pilates
    }
    public class Session : DataEntity
    {
        public int SessionID { get; set; }
        public SessionType? SessionType { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int Room { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
        public virtual ICollection<SessionEnrollment> SessionEnrollments { get; set; }
        public virtual Coach Coach { get; set; }
        public int? CoachID { get; set; }
    }
}
