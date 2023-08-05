using api.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace api.Data
{
    public class CosmosSQLDatabase : IDatabaseAdapter
    {
        public CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public CosmosSQLDatabase()
        {
            string endpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT");
            string key = Environment.GetEnvironmentVariable("COSMOS_KEY");

            // Initialize CosmosClient
            _cosmosClient = new CosmosClient(endpoint, key);

            // Call the async initialization method.
            InitializeAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync("whiskey");
            _container = await _database.CreateContainerIfNotExistsAsync("reviews", "/whiskeyId");
        }

        public async Task<List<WhiskeyReview>> GetWhiskeyReviews(string whiskeyId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.whiskeyId = @whiskeyId")
                .WithParameter("@whiskeyId", whiskeyId);
            FeedIterator<WhiskeyReview> resultSet = _container.GetItemQueryIterator<WhiskeyReview>(query);

            List<WhiskeyReview> results = new List<WhiskeyReview>();
            
            while (resultSet.HasMoreResults)
            {
                FeedResponse<WhiskeyReview> response = await resultSet.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<WhiskeyReview> GetWhiskeyReview(string whiskeyId, string id)
        {
            try
            {

                ItemResponse<WhiskeyReview> response = await _container.ReadItemAsync<WhiskeyReview>(id, new PartitionKey(whiskeyId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> CreateWhiskeyReview(WhiskeyReview whiskeyReview)
        {
            try
            {
                ItemResponse<WhiskeyReview> response = await _container.CreateItemAsync<WhiskeyReview>(whiskeyReview);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateWhiskeyReview(string whiskeyId, string id, WhiskeyReview whiskeyReview)
        {
            try
            {
                ItemResponse<WhiskeyReview> response = await _container.UpsertItemAsync<WhiskeyReview>(whiskeyReview);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteWhiskeyReview(string whiskeyId, string id)
        {
            try
            {
                ItemResponse<WhiskeyReview> response = await _container.DeleteItemAsync<WhiskeyReview>(id, new PartitionKey(whiskeyId));
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

    }
}
