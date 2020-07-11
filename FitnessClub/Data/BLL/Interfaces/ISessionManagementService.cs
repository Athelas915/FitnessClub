using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ISessionManagementService
    {
        Task<SessionViewModel> GetById(int sessionId);
        Task<IList<SessionViewModel>> GetAllSessions();
        Task<IList<SessionViewModel>> GetSessionsForMonth(int year, int month);
        Task CreateSession(SessionViewModel session);
        Task UpdateSession(SessionViewModel session);
        Task DeleteSession(SessionViewModel session);
    }
}
