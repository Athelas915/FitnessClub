using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IPersonService
    {
        AddressViewModel ViewAddress(int userId);
        Task<bool> UpdateAddress(int userId, AddressViewModel inputAddress);
    }
}
