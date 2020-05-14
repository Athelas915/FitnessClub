using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FitnessClub.Data.Models.ViewModels
{
    public class SessionViewModel
    {
        public int SessionID { get; set; }
        [DisplayName("Session Type")]
        public SessionType? SessionType { get; set; }
        [DisplayName("Coach")]
        public string CoachFullname { get; set; }
        public string Weekday { get; set; }
        public string Date { get; set; }
        [DisplayName("Start Time")]
        public string StartTime { get; set; }
        [DisplayName("Finish Time")]
        public string FinishTime { get; set; }
        public int Room { get; set; }
        public SessionViewModel(Session session)
        {
            SessionID = session.SessionID;
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