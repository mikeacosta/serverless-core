using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Controllers;
using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;
using ServerlessCore.Tests.FakeServices;
using Xunit;

namespace ServerlessCore.Tests
{
    public class JobsControllerTest
    {
        IJobService _service;

        public JobsControllerTest()
        {
            _service = new FakeJobService();
        }

        [Fact]
        public async Task GetJobsTest()
        {
            // Arrange
            var controller = new JobsController(_service);

            // Act
            var result = await controller.GetJobs();

            // Assert
            Assert.IsType<ActionResult<CollectionDto<Job>>>(result);
            Assert.Equal(2, result.Value.Items.Count);
        }

        [Fact]
        public async Task AddJobTest()
        {
            // Arrange
            var controller = new JobsController(_service);
            var job = new Job() { Id = 3, Company = "Los Pollos Hermanos" };

            // Act
            var result = await controller.AddJob(job);
            var jobs = await controller.GetJobs();

            // Assert
            Assert.IsType<ActionResult<Job>>(result);
            Assert.Equal(3, jobs.Value.Items.Count);
        }

        [Fact]
        public async Task AddJobBadRequestTest()
        {
            // Arrange
            var controller = new JobsController(_service);
            var job = new Job() { Id = 2, Company = "Big Kahuna Burger" };

            // Act
            var result = await controller.AddJob(job);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProfileItemTest()
        {
            // Arrange
            var controller = new JobsController(_service);
            var job = new Job() { Id = 2, Company = "Big Kahuna Burger" };

            // Act
            var result = await controller.DeleteJob(job);
            var jobs = await controller.GetJobs();

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(1, jobs.Value.Items.Count);
        }

        [Fact]
        public async Task DeleteJobNotFoundTest()
        {
            // Arrange
            var controller = new JobsController(_service);
            var job = new Job() { Id = 4, Company = "Never worked here" };

            // Act
            var result = await controller.DeleteJob(job);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}