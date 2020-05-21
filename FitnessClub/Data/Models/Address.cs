using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data.Models
{
    public class Address : DataEntity
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public virtual Person Person { get; set; }
        public int PersonID { get; set; }
    }
}
