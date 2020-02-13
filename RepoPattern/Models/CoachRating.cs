namespace FC.Data.Models
{
    class CoachRating : BaseEntity
    {
        public int RatingID { get; set; }
        public int CoachID { get; set; }
        public virtual Coach Coach { get; set; }
        public int Rating { get; set; }
    }
}
