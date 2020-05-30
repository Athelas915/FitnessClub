using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CoachViewModel : PersonViewModel<Coach>
    {
        public CoachViewModel(Coach coach) : base(coach) { }
    }
}
