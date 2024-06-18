using Microsoft.Azure.Cosmos;
using ProgramApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramApplication.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Application app)
        {
            await _container.CreateItemAsync(app, new PartitionKey(app.Id));
        }
        public async Task AddItemsAsync(ApplicationResponse app)
        {
            await _container.CreateItemAsync(app, new PartitionKey(app.Id));
        }

        public async Task<Application?> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Application> response = await _container.ReadItemAsync<Application>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Application>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Application>(new QueryDefinition(queryString));
            List<Application> results = new List<Application>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync(string id, Application app)
        {
            await _container.UpsertItemAsync(app, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<Application>(id, new PartitionKey(id));
        }
    }
}
