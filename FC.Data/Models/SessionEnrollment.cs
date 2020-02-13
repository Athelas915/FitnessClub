namespace FC.Data.Models
{
    class SessionEnrollment : BaseEntity
    {
        public int EnrollmentID { get; set; }
        public int SessionID { get; set; }
        public int CustomerID { get; set; }
        public virtual Session Session { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
