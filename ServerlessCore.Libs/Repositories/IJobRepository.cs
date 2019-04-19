using ServerlessCore.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessCore.Libs.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobData>> GetAllJobs();

        Task<JobData> GetJob(int id);

        Task AddJob(JobData job);

        Task DeleteJob(JobData job);
    }
}
