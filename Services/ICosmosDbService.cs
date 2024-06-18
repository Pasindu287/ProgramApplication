using ProgramApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProgramApplication.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Application>> GetItemsAsync(string query);
        Task<Application> GetItemAsync(string id);
        Task AddItemAsync(Application app);
        Task AddItemsAsync(ApplicationResponse app);
        Task UpdateItemAsync(string id, Application app);
        Task DeleteItemAsync(string id);
    }
}
