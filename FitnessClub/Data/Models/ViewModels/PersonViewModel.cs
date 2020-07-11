using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FitnessClub.Data.Models.Gender;

namespace FitnessClub.Data.Models.ViewModels
{
    public abstract class PersonViewModel<PEntity> : ViewModel<PEntity> where PEntity : Person, new()
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public PersonViewModel() {}
        public PersonViewModel(PEntity person) : base(person) {}
        public int PersonID 
        {
            get => model.PersonID;
        }
        public string FullName 
        {
            get => $"{model.FirstName} {model.LastName}".Trim();
            set
            {
                if (value == null)
                {
                    model.FirstName = model.LastName = null;
                    return;
                }
                var items = value.Split();
                if (items.Length > 0)
                    model.FirstName = items[0]; // may cause npc
                if (items.Length > 1)
                    model.LastName = items[1];
            }
        }
        public string Gender 
        {
            get => model.Gender.ToString();
            set
            {
                if (value.ToLower() == Male.ToString().ToLower())
                {
                    model.Gender = Male;
                }
                else if (value.ToLower() == Female.ToString().ToLower())
                {
                    model.Gender = Female;
                }
                else
                {
                    model.Gender = null;
                }
            }
        }
        public string Birthdate
        {
            get => model.Birthdate.ToShortDateString();
            set => model.Birthdate = DateTime.Parse(value);
        }
        public virtual AddressViewModel Address 
        {
            get => model.Address != null ? new AddressViewModel(model.Address) : null;
            set => model.Address = value.Model;
        }
    }
    public class PersonViewModel : PersonViewModel<Person>
    {
        public PersonViewModel() {}
        public PersonViewModel(Person person) : base(person) {}
    }
}