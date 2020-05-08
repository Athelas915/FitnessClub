using System;

namespace FitnessClub.Data.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class Person : BaseEntity
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public DateTime Birthdate { get; set; }

    }
}
