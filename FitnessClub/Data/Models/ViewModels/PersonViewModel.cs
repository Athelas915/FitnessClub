﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public abstract class PersonViewModel<PEntity> where PEntity : Person
    {
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
}
