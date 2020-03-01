namespace FitnessClub.Data.Models
{
    public class Coach : Employee
    {
        public int CoachID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
