using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FitnessClub.Data.Models.ViewModels
{
    public class MembershipViewModel : ViewModel<Membership>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public MembershipViewModel() {}
        public MembershipViewModel(Membership membership) : base(membership) {}
        public int MembershipID 
        {
            get => model.MembershipID;
        }
        [DisplayName("Membership Number")]
        public int MembershipNo 
        {
            get => model.MembershipNo;
            set => model.MembershipNo = value;
        }
        [DisplayName("Start Date")]
        public string Start 
        { 
            get => model.Start.ToShortDateString();
            set => model.Start = DateTime.Parse(value); 
        }
        [DisplayName("Expiration Date")]
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
                if (dlStart< 0)
                {
                    var dlEnd = DateTime.Now.Subtract(model.Finish).Days;
                    if (dlEnd< 0)
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
        public string CustomerFirstName
        {
            get => model.Customer.FirstName;
            set => model.Customer.FirstName = value;
        }
        public string CustomerLastName
        {
            get => model.Customer.LastName;
            set => model.Customer.LastName = value;
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