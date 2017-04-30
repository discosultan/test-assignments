using Microsoft.WindowsAzure.Storage.Table;

namespace Varus.Stopwatch.AzureTableStorage
{
    internal class TimestampEntity : TableEntity
    {
        public TimestampEntity(string user, string name, long ticks)
        {
            PartitionKey = user;
            RowKey = name;
            Ticks = ticks;
        }

        public TimestampEntity() { }

        public long Ticks { get; set; }
    }
}
