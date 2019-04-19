using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessCore.Tests.FakeServices
{
    public class FakeJobService : IJobService
    {
        private readonly CollectionDto<Job> _jobs = new CollectionDto<Job>();

        public FakeJobService()
        {
            _jobs = new CollectionDto<Job>();
            _jobs.Items.Add(new Job() { Id = 1, Company = "Acme Widgets" });
            _jobs.Items.Add(new Job() { Id = 2, Company = "Big Kahuna Burger" });
        }

        public async Task AddJobAsync(Job job)
        {
            await Task.Run(() => _jobs.Items.Add(job));
        }

        public async Task DeleteJobAsync(Job job)
        {
            await Task.Run(() => _jobs.Items.Remove(job));
        }

        public async Task<Job> GetJobAsync(int id)
        {
            var job = _jobs.Items.FirstOrDefault(j => j.Id == id);
            return await Task.FromResult(job);
        }

        public async Task<CollectionDto<Job>> GetJobsAsync()
        {
            return await _jobs;
        }
    }
}
