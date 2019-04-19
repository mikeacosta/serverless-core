using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ServerlessCore.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessCore.Libs.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DynamoDBContext _context;

        public JobRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<JobData>> GetAllJobs()
        {
            return await _context.ScanAsync<JobData>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<JobData> GetJob(int id)
        {
            return await _context.LoadAsync<JobData>(id);
        }

        public async Task AddJob(JobData job)
        {
            await _context.SaveAsync(job);
        }

        public async Task DeleteJob(JobData job)
        {
            await _context.DeleteAsync<JobData>(job);
        }
    }
}
