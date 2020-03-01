namespace FitnessClub.Data.Models
{
    public class Adress : BaseEntity
    {
        public virtual Person Person { get; set; }
        public int AdressID { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
