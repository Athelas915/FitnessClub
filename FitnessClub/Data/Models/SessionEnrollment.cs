namespace FitnessClub.Data.Models
{
    public class SessionEnrollment : DataEntity
    {
        public virtual Session Session { get; set; }
        public int SessionID { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
