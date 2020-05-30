using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data.Models
{
    public class DataEntity : BaseEntity
    {
        public int CreatedBy { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
        
    }
}
