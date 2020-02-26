using System;
using System.ComponentModel;

namespace FitnessClub.Data.Models
{
    public class BaseEntity
    {
        [ReadOnly(true)]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
