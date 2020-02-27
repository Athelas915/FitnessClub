namespace FitnessClub.Data.Models
{
    public class CoachRating : BaseEntity
    {
        public int CoachRatingID { get; set; }
        public int PersonID { get; set; } // PersonID of the coach.
        public virtual Coach Coach { get; set; }
        public int Rating { get; set; }
    }
}
