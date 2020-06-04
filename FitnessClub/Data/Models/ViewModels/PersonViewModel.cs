using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public abstract class PersonViewModel<PEntity> where PEntity : Person
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public PersonViewModel()
        {

        }
        public PersonViewModel(PEntity person)
        {
            PersonID = person.PersonID;
            FullName = person.FirstName + " " + person.LastName;
            Gender = person.Gender;
            Birthdate = person.Birthdate.ToShortTimeString();
            Address = new AddressViewModel(person.Address);
        }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public Gender? Gender { get; set; }
        public string Birthdate { get; set; }
        public virtual AddressViewModel Address { get; set; }
    }
    public class PersonViewModel : PersonViewModel<Person>
    {
        public PersonViewModel(Person person) : base(person)
        {
        }
    }
}
