using ServerlessCore.Data.Models;
using System.Threading.Tasks;

namespace ServerlessCore.Services
{
    public interface IJobService
    {
        Task<CollectionDto<Job>> GetJobsAsync();

        Task<Job> GetJobAsync(int id);

        Task AddJobAsync(Job job);

        Task DeleteJobAsync(Job job);
    }
}
