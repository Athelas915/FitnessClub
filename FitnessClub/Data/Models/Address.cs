using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data.Models
{
    public class Address : DataEntity
    {
        public int AddressID { get; set; }
        [ProtectedPersonalData]
        public string Street { get; set; }
        [ProtectedPersonalData]
        public string ZipCode { get; set; }
        [ProtectedPersonalData]
        public string City { get; set; }
        [ProtectedPersonalData]
        public string Region { get; set; }
        [ProtectedPersonalData]
        public string Country { get; set; }
        public virtual Person Person { get; set; }
        public int PersonID { get; set; }
    }
}
