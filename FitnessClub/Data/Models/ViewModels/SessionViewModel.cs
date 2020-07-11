using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data.Models.ViewModels
{
    public class SessionViewModel : ViewModel<Session>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public SessionViewModel() { }
        public SessionViewModel(Session session) : base(session) {}
        public int SessionID
        {
            get => model.SessionID;
        }
        [DisplayName("Session Type")]
        public SessionType? SessionType
        {
            get => model.SessionType;
            set => model.SessionType = value;
        }
        public string Date
        {
            get => model.Start.ToShortDateString();
            set
            {
                model.Start = DateTime.Parse(value) + model.Start.TimeOfDay;
                model.Finish = model.Start + TimeSpan.Parse(Duration);
            }
        }
        [DisplayName("Start Time")]
        public string StartTime 
        {
            get => model.Start.ToShortTimeString();
            set
            {
                model.Start = model.Start.Date + TimeSpan.Parse(value);
                model.Finish = model.Start + TimeSpan.Parse(Duration);

            }
        }
        public string Duration 
        { 
            get => (model.Finish - model.Start).ToString("hh\\:mm");
            set => model.Finish = model.Start + TimeSpan.Parse(value);
        }

        public string Weekday
        {
            get => DateTime.Parse(StartTime).DayOfWeek.ToString();
        }
        public int Room 
        {
            get => model.Room;
            set => model.Room = value; 
        }
        public IList<int> Ratings
        {
            get
            {
                if (model.CoachRatings != null)
                {
                    return model.CoachRatings.Select(a => a.Rating).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        private CoachViewModel lazyCoach = null;
        public CoachViewModel Coach
        {
            get
            {
                if (model.Coach != null)
                {
                    lazyCoach = lazyCoach == null ? new CoachViewModel(model.Coach) : lazyCoach;
                    return lazyCoach;
                }
                else
                {
                    return null;
                }
            }
        }
        public ICollection<CustomerViewModel> Participants
        {
            get
            {
                if (model.SessionEnrollments != null)
                {
                    var result = new List<CustomerViewModel>();
                    foreach (var se in model.SessionEnrollments)
                    {
                        if (se.Customer != null)
                        {
                            result.Add(new CustomerViewModel(se.Customer));
                        }
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
