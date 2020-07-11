namespace FitnessClub.Data.Models
{
    public class SessionEnrollment : DataEntity
    {
        public SessionEnrollment() {}
        public SessionEnrollment(SessionEnrollment se)
        {
            CreatedOn = se.CreatedOn;
            CreatedBy = se.CreatedBy;
            SessionID = se.SessionID;
            CustomerID = se.CustomerID;
            if (se.Session != null)
            {
                Session = new Session(se.Session);
            }
            if (se.Customer != null)
            {
                Customer = new Customer(se.Customer);
            }
        }
        public virtual Session Session { get; set; }
        public int SessionID { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
