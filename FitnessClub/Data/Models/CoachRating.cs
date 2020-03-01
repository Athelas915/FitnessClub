namespace FitnessClub.Data.Models
{
    public class CoachRating : BaseEntity
    {
        public int CoachRatingID { get; set; }
        public int CoachID { get; set; }
        public virtual Coach Coach { get; set; }
        public int Rating { get; set; }
    }
}
