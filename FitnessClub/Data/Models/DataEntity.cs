using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data.Models
{
    public abstract class DataEntity : BaseEntity
    {
        public DataEntity() { }

        public DataEntity(DataEntity de) 
        {
            CreatedBy = de.CreatedBy;
            CreatedOn = de.CreatedOn;
        }

        public int CreatedBy { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
        
    }
}
