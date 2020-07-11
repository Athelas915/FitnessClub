using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CustomerViewModel : PersonViewModel<Customer>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public CustomerViewModel() {}
        public CustomerViewModel(Customer customer) : base(customer) { }
        public ICollection<MembershipViewModel> Memberships
        {
            get => model.Memberships?.Select(a => new MembershipViewModel(a)).ToList();
        }
        public IDictionary<SessionViewModel, int?> Sessions
        {
            get
            {
                var result = model.SessionEnrollments.Select(
                    a => 
                    new { 
                        session = a.Session, 
                        rating = model
                        .CoachRatings
                        .FirstOrDefault(b => a.SessionID == b.SessionID)?
                        .Rating 
                    })
                    .ToDictionary( x => 
                    new SessionViewModel(x.session), 
                    x=> x.rating
                    );

                return result;
            }
        }
    }
}
