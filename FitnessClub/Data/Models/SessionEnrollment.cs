namespace FitnessClub.Data.Models
{
    public class SessionEnrollment : BaseEntity
    {
        public int? SessionID { get; set; }
        public int? PersonID { get; set; }
        public virtual Session Session { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
