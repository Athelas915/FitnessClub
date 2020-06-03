using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly ILogger<PersonService> logger;
        public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger)
        {
            this.personRepository = personRepository;
            this.logger = logger;
        }

        public async Task<bool> UpdateAddress(int userId, AddressViewModel inputAddress)
        {
            var personId = personRepository.GetPersonIdByUserId(userId);
            var person = personRepository.FindWithAddress(personId);
            if (person == null || person.Address == null)
            {
                logger.LogInformation($"Couldn't find the person or address with given userId {userId}.");
                return false;
            }
            person.Address.City = inputAddress.City;
            person.Address.Country = inputAddress.Country;
            person.Address.Region = inputAddress.Region;
            person.Address.Street = inputAddress.Street;
            person.Address.ZipCode = inputAddress.ZipCode;

            personRepository.Update(person);

            await personRepository.Commit();
            return true;
        }

        public AddressViewModel ViewAddress(int userId)
        {
            var personId = personRepository.GetPersonIdByUserId(userId);
            var person = personRepository.FindWithAddress(personId);
            if (person == null || person.Address == null)
            {
                logger.LogInformation($"Couldn't find the person or address with given userId {userId}.");
                return null;
            }
            return new AddressViewModel(person.Address);
        }
    }
}
