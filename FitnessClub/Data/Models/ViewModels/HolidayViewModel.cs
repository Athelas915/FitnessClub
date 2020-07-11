using System;
using System.ComponentModel;

namespace FitnessClub.Data.Models.ViewModels
{
    public class HolidayViewModel : ViewModel<Holiday>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public HolidayViewModel() {}
        public HolidayViewModel(Holiday holiday) : base(holiday) {}
        public int HolidayID 
        {
            get => model.HolidayID;
        }
        [DisplayName("Start Date")]
        public string Start
        {
            get => model.Start.ToShortDateString();
            set => model.Start = DateTime.Parse(value);
        }
        [DisplayName("Finish Date")]
        public string Finish
        {
            get => model.Finish.ToShortDateString();
            set => model.Finish = DateTime.Parse(value);
        }
        public int DaysLeft
        {
            get
            {
                var dlStart = DateTime.Now.Subtract(model.Start).Days;
                if (dlStart < 0)
                {
                    var dlEnd = DateTime.Now.Subtract(model.Finish).Days;
                    if (dlEnd < 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return dlEnd;
                    }
                }
                else
                {
                    return dlStart;
                }
            }
        }
        public bool Active
        {
            get
            {
                var result = model.Start < DateTime.Now && model.Finish > DateTime.Now ? true : false;
                return result;
            }
        }
    }
}
