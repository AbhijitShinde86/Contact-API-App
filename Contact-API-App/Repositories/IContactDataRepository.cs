using Contact_API_App.Models;
using System.Threading.Tasks;

namespace Contact_API_App.Repositories
{
    public interface IContactDataRepository
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(int id);
        Task<Contact> Add(Contact newContact);
        Task<Contact> Update(Contact updatedContact);
        Task<bool> Delete(int id);
    }
}
