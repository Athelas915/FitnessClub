using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FitnessClub.Data.Models.ViewModels
{
    public class SessionViewModel
    {
        public SessionViewModel(Session session)
        {
            SessionID = session.SessionID;
            SessionType = session.SessionType;
            var firstName = session.Coach.FirstName;
            var lastName = session.Coach.LastName;
            CoachFullName = firstName + ' ' + lastName;
            Weekday = session.Start.DayOfWeek.ToString();
            Date = session.Start.ToShortDateString();
            StartTime = session.Start.ToShortTimeString();
            FinishTime = session.Finish.ToShortTimeString();
            Room = session.Room;
        }
        public int SessionID { get; set; }
        [DisplayName("Session Type")]
        public SessionType? SessionType { get; set; }
        [DisplayName("Coach")]
        public string CoachFullName{ get; set; }
        public string Weekday { get; set; }
        public string Date { get; set; }
        [DisplayName("Start Time")]
        public string StartTime { get; set; }
        [DisplayName("Finish Time")]
        public string FinishTime { get; set; }
        public int Room { get; set; }
    }
}