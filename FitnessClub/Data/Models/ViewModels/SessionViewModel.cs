using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class SessionViewModel
    {
        public SessionType? SessionType { get; set; }
        public string CoachFullname { get; set; }
        public string Weekday { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public int Room { get; set; }
        public SessionViewModel(Session session)
        {
            SessionType = session.SessionType;
            CoachFullname = session.Coach.FirstName + " " + session.Coach.LastName;
            Weekday = session.Start.DayOfWeek.ToString();
            Date = session.Start.ToShortDateString();
            StartTime = session.Start.ToShortTimeString();
            FinishTime = session.Finish.ToShortTimeString();
            Room = session.Room;
        }
    }
}