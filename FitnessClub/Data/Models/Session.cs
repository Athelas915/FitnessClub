using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitnessClub.Data.Models
{
    public enum SessionType
    {
        Aerobics, Spinning, Boxing, Yoga, Pilates
    }
    public class Session : DataEntity
    {
        public Session() {}
        public Session(Session s) : base(s)
        {
            SessionID = s.SessionID;
            SessionType = (SessionType)(int)s.SessionType;
            Start = s.Start;
            Finish = s.Finish;
            Room = s.Room;
            CoachID = s.CoachID;
            if (s.CoachRatings != null)
            {
                CoachRatings = new List<CoachRating>(s.CoachRatings);
            }
            if (s.SessionEnrollments != null)
            {
                SessionEnrollments = new List<SessionEnrollment>(s.SessionEnrollments);
            }
            if (s.Coach != null)
            {
                Coach = new Coach(s.Coach);
            }
        }
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
