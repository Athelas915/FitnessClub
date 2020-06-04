using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CoachViewModel : PersonViewModel<Coach>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public CoachViewModel()
        {

        }
        public CoachViewModel(Coach coach) : base(coach) { }
    }
}
