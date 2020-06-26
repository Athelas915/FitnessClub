using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data.Models.ViewModels
{
    public class SessionViewModel
    {
        private readonly Session session;
        //The parameterless constructor is required for Model Binding on razor pages.
        public SessionViewModel()
        {
            session = new Session();
            session.Coach = new Coach();
        }
        public SessionViewModel(Session session)
        {
            this.session = session;
            if (this.session.Coach == null)
            {
                this.session.Coach = new Coach();
            }
        }
        public int SessionID
        {
            get => session.SessionID;
            set => session.SessionID = value;
        }
        [DisplayName("Session Type")]
        public SessionType? SessionType
        {
            get => session.SessionType;
            set => session.SessionType = value;
        }
        public string CoachFirstName
        {
            get
            {
                return session.Coach.FirstName;
            }
            set
            {
                session.Coach.FirstName = value;
            }
        }
        public string CoachLastName
        {
            get
            {
                return session.Coach.LastName;
            }
            set
            {
                session.Coach.LastName = value;
            }
        }
        [DisplayName("Coach")]
        public string CoachFullName{
            get => $"{CoachFirstName} {CoachLastName}".Trim();
            set
            {
                if (value == null)
                {
                    CoachFirstName = CoachLastName = null;
                    return;
                }
                var items = value.Split();
                if (items.Length > 0)
                    CoachFirstName = items[0]; // may cause npc
                if (items.Length > 1)
                    CoachLastName = items[1];
            }
        }
        public string Date
        {
            get => session.Start.ToShortDateString();
            set
            {
                session.Start = DateTime.Parse(value) + session.Start.TimeOfDay;
                session.Finish = session.Start + TimeSpan.Parse(Duration);
            }
        }
        [DisplayName("Start Time")]
        public string StartTime 
        {
            get => session.Start.ToShortTimeString();
            set
            {
                session.Start = session.Start.Date + TimeSpan.Parse(value);
                session.Finish = session.Start + TimeSpan.Parse(Duration);

            }
        }
        public string Duration 
        { 
            get => (session.Finish - session.Start).ToString("hh\\:mm");
            set => session.Finish = session.Start + TimeSpan.Parse(value);
        }

        public string Weekday
        {
            get => DateTime.Parse(StartTime).DayOfWeek.ToString();
        }
        public int Room 
        {
            get => session.Room;
            set => session.Room = value; 
        }
    }
}