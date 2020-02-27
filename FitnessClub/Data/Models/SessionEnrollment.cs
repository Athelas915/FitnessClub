namespace FitnessClub.Data.Models
{
    public class SessionEnrollment : BaseEntity
    {
        public int SessionEnrollmentID { get; set; }
        public int? SessionID { get; set; }
        public int? PersonID { get; set; }
        public virtual Session Session { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
