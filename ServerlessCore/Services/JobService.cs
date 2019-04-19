using System.Linq;
using System.Threading.Tasks;
using ServerlessCore.Data.Models;
using ServerlessCore.Libs.Mappers;
using ServerlessCore.Libs.Repositories;

namespace ServerlessCore.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repo;
        private readonly IMapper _mapper;

        public JobService(IJobRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CollectionDto<Job>> GetJobsAsync()
        {
            var response = await _repo.GetAllJobs();
            var jobs = _mapper.ToJob(response).OrderBy(j => j.Id).ToList();
            return new CollectionDto<Job> { Items = jobs };
        }

        public async Task<Job> GetJobAsync(int id)
        {
            var response = await _repo.GetJob(id);
            if (response == null)
                return null;

            return _mapper.ToJob(response);
        }

        public async Task AddJobAsync(Job job)
        {
            await _repo.AddJob(_mapper.ToJobData(job));
        }

        public async Task DeleteJobAsync(Job job)
        {
            await _repo.DeleteJob(_mapper.ToJobData(job));
        }
    }
}
