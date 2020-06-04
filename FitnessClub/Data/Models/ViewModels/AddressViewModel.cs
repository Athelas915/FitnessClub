namespace FitnessClub.Data.Models.ViewModels
{
    public class AddressViewModel
    {
        //The parameterless constructor is required for Model Binding on razor pages. 
        public AddressViewModel()
        {

        }
        public AddressViewModel(Address address)
        {
            AddressID = address.AddressID;
            Street = address.Street;
            ZipCode = address.ZipCode;
            City = address.City;
            Region = address.Region;
            Country = address.Country;
        }
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
