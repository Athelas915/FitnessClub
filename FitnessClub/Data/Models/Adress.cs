using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Data.Models
{
    public class Adress : BaseEntity
    {
        public virtual Person Person { get; set; }
        [Key]
        public int PersonID { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
