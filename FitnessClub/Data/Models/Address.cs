using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data.Models
{
    public class Address : DataEntity
    {
        public Address() { }

        public Address(Address a) : base(a)
        {
            AddressID = a.AddressID;
            Street = a.Street;
            ZipCode = a.ZipCode;
            City = a.City;
            Region = a.Region;
            Country = a.Country;
            PersonID = a.PersonID;
            if (a.Person != null)
            {
                Person = new Person(a.Person);
            }
        }

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
