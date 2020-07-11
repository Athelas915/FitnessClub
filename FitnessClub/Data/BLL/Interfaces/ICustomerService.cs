using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task CreateMembership(CustomerViewModel customer, MembershipViewModel membership);
        Task UpdateMembership(CustomerViewModel customer, MembershipViewModel membership);
        Task DeleteMembership(CustomerViewModel customer, MembershipViewModel membership);
        Task<CustomerViewModel> GetCustomer(int userId);
        Task<CustomerViewModel> GetWithMemberships(int userId);
        Task<CustomerViewModel> GetWithSessions(int userId);
        Task RateCoach(SessionViewModel session, CustomerViewModel customer, int rating);
    }
}
