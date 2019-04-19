using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ServerlessCore.Libs.Models;

namespace ServerlessCore.Libs.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly DynamoDBContext _context;

        public ContentRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<ContentData>> GetAllItems()
        {
            return await _context.ScanAsync<ContentData>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<ContentData> GetItem(string name)
        {
            return await _context.LoadAsync<ContentData>(name);
        }

        public async Task AddItem(ContentData item)
        {
            await _context.SaveAsync(item);
        }

        public async Task DeleteItem(ContentData item)
        {
            await _context.DeleteAsync<ContentData>(item);
        }
    }
}
