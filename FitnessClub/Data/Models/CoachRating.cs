namespace FitnessClub.Data.Models
{
    public class CoachRating : BaseEntity
    {
        public int CoachRatingID { get; set; }
        public int Rating { get; set; }
        public virtual Session Session { get; set; }
        public int SessionID { get; set; }
    }
}
