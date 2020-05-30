namespace FitnessClub.Data.Models
{
    public class CoachRating : DataEntity
    {
        public int CoachRatingID { get; set; }
        public int Rating { get; set; }
        public virtual Coach Coach { get; set; }
        public int CoachID { get; set; }
        public virtual Customer Customer { get; set; }
        public int? CustomerID { get; set; }
        public virtual Session Session { get; set; }
        public int SessionID { get; set; }
    }
}
