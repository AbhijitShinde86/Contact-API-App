using Contact_API_App.Models;
using System.Text.Json;

namespace Contact_API_App.Repositories
{
    public class JsonContactDataRepository : IContactDataRepository
    {
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");
        private readonly ILogger<JsonContactDataRepository> _logger;

        public JsonContactDataRepository(ILogger<JsonContactDataRepository> logger)
        {
            _logger = logger;
        }

        public async Task<List<Contact>> GetAll()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<Contact>();

                return await GetAllRecordsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading file: {ex.Message}");
                throw new Exception("An error occurred while reading data.");
            }
        }

        public async Task<Contact> GetById(int id)
        {
            var contacts = await GetAllRecordsAsync();
            return contacts.FirstOrDefault(u => u?.Id == id);
        }

        public async Task<Contact> Add(Contact newContact)
        {
            try
            {
                var contacts = await GetAllRecordsAsync();
                newContact.Id = contacts.Any() ? contacts.Max(u => u.Id) + 1 : 1; // Auto-increment ID
                contacts.Add(newContact);

                await SaveContactsAsync(contacts);
                return newContact;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding contact: {ex.Message}");
                throw new Exception("An error occurred while adding contact.");
            }
           
        }

        public async Task<Contact> Update(Contact updatedContact)
        {
            try
            {
                var contacts = await GetAllRecordsAsync();
                var contact = contacts.FirstOrDefault(u => u.Id == updatedContact.Id);

                if (contact == null)
                {
                    throw new KeyNotFoundException($"Contact with ID {updatedContact.Id} not found.");
                }

                contact.FirstName = updatedContact.FirstName;
                contact.LastName = updatedContact.LastName;
                contact.Email = updatedContact.Email;

                await SaveContactsAsync(contacts);
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating contact: {ex.Message}");
                throw new Exception("An error occurred while updating contact.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var contacts = await GetAllRecordsAsync();
                var contact = contacts.FirstOrDefault(u => u.Id == id);

                if (contact == null)
                {
                    throw new KeyNotFoundException($"Contact with ID {id} not found.");
                }

                contacts.Remove(contact);
                await SaveContactsAsync(contacts);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting contact: {ex.Message}");
                throw new Exception("An error occurred while deleting contact.");
            }
        }

        #region private
            private async Task<List<Contact>> GetAllRecordsAsync()
            {
                var jsonData = await File.ReadAllTextAsync(_filePath);
                var data = JsonSerializer.Deserialize<List<Contact>>(jsonData);
                return data ?? new List<Contact>();
            }
            private async Task SaveContactsAsync(List<Contact> contacts)
            {
                var jsonData = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, jsonData);
            }
        #endregion
    }
}
