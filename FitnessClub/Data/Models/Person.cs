using System;

namespace FitnessClub.Data.Models
{
    public class Person : BaseEntity
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; } // 0 for male, 1 for female, to be implemented in a better way later
        public DateTime Birthdate { get; set; }

    }
}
