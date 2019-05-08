using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;

namespace ServerlessCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CollectionDto<Job>>> GetJobs()
        {
            var jobs = await _jobService.GetJobsAsync();
            return jobs;
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobService.GetJobAsync(id);
            return job;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Job>> AddJob([FromBody] Job job)
        {
            var existingJob = await _jobService.GetJobAsync(job.Id);

            if (existingJob != null)
                return BadRequest();

            await _jobService.AddJobAsync(job);
            return CreatedAtAction(nameof(AddJob), job);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJob([FromBody] Job job)
        {
            var existingJob = await _jobService.GetJobAsync(job.Id);

            if (existingJob == null)
                return NotFound();

            await _jobService.DeleteJobAsync(existingJob);
            return Ok();
        }
    }
}