using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ServerlessCore.Libs.Models;

namespace ServerlessCore.Libs.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DynamoDBContext _context;

        public ProfileRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<ProfileData>> GetAllItems()
        {
            return await _context.ScanAsync<ProfileData>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<ProfileData> GetItem(int id)
        {
            return await _context.LoadAsync<ProfileData>(id);
        }

        public async Task AddItem(ProfileData item)
        {
            await _context.SaveAsync(item);
        }

        public async Task DeleteItem(ProfileData item)
        {
            await _context.DeleteAsync<ProfileData>(item);
        }
    }
}
