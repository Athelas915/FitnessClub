using System;
using FitnessClub.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace FitnessClub.Data.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Person : DataEntity
    {
        public Person() { }

        public Person(Person p) : base(p)
        {
            PersonID = p.PersonID;
            FirstName = p.FirstName;
            LastName = p.LastName;
            Gender = (Gender)(int)p.Gender;
            Birthdate = p.Birthdate;
            AspNetUser = p.AspNetUser;
            UserID = p.UserID;
            if (p.Address != null)
            {
                Address = new Address(p.Address);
            }
        }

        public int PersonID { get; set; }
        [ProtectedPersonalData]
        public string FirstName { get; set; }
        [ProtectedPersonalData]
        public string LastName { get; set; }
        [ProtectedPersonalData]
        public Gender? Gender { get; set; }
        [ProtectedPersonalData]
        public DateTime Birthdate { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public int UserID { get; set; }
        public virtual Address Address { get; set; }
    }
}
