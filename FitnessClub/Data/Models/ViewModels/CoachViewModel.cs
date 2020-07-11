using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CoachViewModel : EmployeeViewModel<Coach>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public CoachViewModel() { }
        public CoachViewModel(Coach coach) : base(coach) { }
        public ICollection<SessionViewModel> Sessions
        {
            get => model.Sessions?.Select(a => new SessionViewModel(a)).ToList();
        }
        public float? AverageRating
        {
            get
            {
                if (model.CoachRatings != null)
                {
                    var i = 0;
                    var result = 0;
                    foreach (var r in model.CoachRatings)
                    {
                        result += r.Rating;
                        i++;
                    }
                    return result / i;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
