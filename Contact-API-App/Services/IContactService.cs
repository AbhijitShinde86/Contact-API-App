using Contact_API_App.Models;

namespace Contact_API_App.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> CreateContactAsync(Contact newContact);
        Task<Contact> UpdateContactAsync(int id, Contact updatedContact);
        Task<bool> DeleteContactAsync(int id);
    }
}
