using Contact_API_App.Models;
using Contact_API_App.Repositories;

namespace Contact_API_App.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactDataRepository _contactDataRepository;
        public ContactService(IContactDataRepository contactDataRepository)
        {
            _contactDataRepository = contactDataRepository;
        }
        public async Task<List<Contact>> GetContactsAsync()
        {
            try
            {
                return await _contactDataRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            try
            {
                return await _contactDataRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Contact> CreateContactAsync(Contact newContact)
        {
            try
            {
                return _contactDataRepository.Add(newContact);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Contact> UpdateContactAsync(int id, Contact updatedContact)
        {
            try
            {
                updatedContact.Id = id;
                return _contactDataRepository.Update(updatedContact);
            }
            catch (Exception ex)
            {
                throw;
            }   
        }
        public Task<bool> DeleteContactAsync(int id)
        {
            try
            {
                return _contactDataRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
