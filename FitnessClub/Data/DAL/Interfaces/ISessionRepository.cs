using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> GetAllWithRatings();
        IEnumerable<Session> GetAllWithEnrollments();
        IEnumerable<Session> GetAllWithCoach();
        Session FindWithRatings(int id);
        Session FindWithEnrollments(int id);
        Session FindWithCoach(int id);
    }
}
