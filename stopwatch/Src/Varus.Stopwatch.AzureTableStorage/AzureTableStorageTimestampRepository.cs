using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Varus.Stopwatch.Repositories;

namespace Varus.Stopwatch.AzureTableStorage
{
    public class AzureTableStorageTimestampRepository : ITimestampRepository
    {
        private readonly CloudTable _table;

        public AzureTableStorageTimestampRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference("Timestamp");
            _table.CreateIfNotExists();
        }

        public async Task AddOrUpdateTimestampAsync(string user, string name, long timestamp)
        {
            var entity = new TimestampEntity(user, name, timestamp);
            var op = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(op);
        }

        public Task<IDictionary<string, long>> MapTimestampsByNameAsync(string user)
        {
            var query = new TableQuery<TimestampEntity>
            {
                FilterString = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user)
            };
            IDictionary<string, long> result = _table.ExecuteQuery(query).ToDictionary(x => x.RowKey, x => x.Ticks);
            return Task.FromResult(result);
        }
    }
}
