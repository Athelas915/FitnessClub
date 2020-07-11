namespace FitnessClub.Data.Models
{
    public class CoachRating : DataEntity
    {
        public CoachRating() { }

        public CoachRating(CoachRating cr) : base(cr)
        {
            CoachRatingID = cr.CoachRatingID;
            Rating = cr.Rating;
            CoachID = cr.CoachID;
            CustomerID = cr.CustomerID;
            SessionID = cr.SessionID;
            if (cr.Coach != null)
            {
                Coach = new Coach(cr.Coach);
            }
            if (cr.Customer != null)
            {
                Customer = new Customer(cr.Customer);
            }
            if (cr.Session != null)
            {
                Session = new Session(cr.Session);
            }
        }

        public int CoachRatingID { get; set; }
        private int rating = 0;
        public int Rating {
            get => rating;
            set
            {
                if (value < 0)
                {
                    rating = 0;
                }
                else if (value > 5)
                {
                    rating = 5;
                }
                else
                {
                    rating = value;
                }
            }
        }
        public virtual Coach Coach { get; set; }
        public int CoachID { get; set; }
        public virtual Customer Customer { get; set; }
        public int? CustomerID { get; set; }
        public virtual Session Session { get; set; }
        public int SessionID { get; set; }
    }
}
