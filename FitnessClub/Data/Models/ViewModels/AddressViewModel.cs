namespace FitnessClub.Data.Models.ViewModels
{
    public class AddressViewModel : ViewModel<Address>
    {
        //The parameterless constructor is required for Model Binding on razor pages. 
        public AddressViewModel() {}
        public AddressViewModel(Address address) : base(address) {}
        public int AddressID { 
            get => model.AddressID;
        }
        public string Street {
            get => model.Street;
            set => model.Street = value;
        }
        public string ZipCode {
            get => model.ZipCode;
            set => model.ZipCode = value;
        }
        public string City {
            get => model.City;
            set => model.City = value;
        }
        public string Region {
            get => model.Region;
            set => model.Region = value;
        }
        public string Country {
            get => model.Country;
            set => model.Country = value;
        }
    }
}
