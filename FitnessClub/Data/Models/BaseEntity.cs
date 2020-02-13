using System;

namespace FitnessClub.Data.Models
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
