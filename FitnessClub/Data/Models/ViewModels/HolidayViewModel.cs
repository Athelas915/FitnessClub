namespace FitnessClub.Data.Models.ViewModels
{
    public class HolidayViewModel
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public HolidayViewModel()
        {

        }
        public HolidayViewModel(Holiday holiday)
        {
            HolidayID = holiday.HolidayID;
            Start = holiday.Finish.ToShortDateString();
            Finish = holiday.Finish.ToShortDateString();
            var firstName = holiday.Employee.FirstName;
            var lastName = holiday.Employee.LastName;
            EmployeeFullName = firstName + ' ' + lastName;
        }
        public int HolidayID { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string EmployeeFullName { get; set; }
    }
}
