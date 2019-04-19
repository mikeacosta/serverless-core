using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Controllers;
using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;
using ServerlessCore.Tests.FakeServices;
using Xunit;

namespace ServerlessCore.Tests
{
    public class ProfileControllerTest
    {
        IProfileService _service;

        public ProfileControllerTest()
        {
            _service = new FakeProfileService();
        }

        [Fact]
        public async Task GetProfileTest()
        {
            // Arrange
            var controller = new ProfileController(_service);

            // Act
            var result = await controller.GetProfile();

            // Assert
            Assert.IsType<ActionResult<CollectionDto<ProfileItem>>>(result);
            Assert.Equal(5, result.Value.Items.Count);
        }

        [Fact]
        public async Task GetProfileItemTest()
        {
            // Arrange
            var controller = new ProfileController(_service);

            // Act
            var result = await controller.GetProfileItem(3);

            // Assert
            Assert.IsType<ActionResult<ProfileItem>>(result);
            Assert.Equal(ProfileItemType.Header, result.Value.Type);
        }

        [Fact]
        public async Task GetProfileItemNotFoundTest()
        {
            // Arrange
            var controller = new ProfileController(_service);

            // Act
            var result = await controller.GetProfileItem(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProfileItemTest()
        {
            // Arrange
            var controller = new ProfileController(_service);
            var item = new ProfileItem() { Id = 50, Type = ProfileItemType.Skill, Text = "Notepad guru" };

            // Act
            var result = await controller.AddProfileItem(item);
            var profile = await controller.GetProfile();

            // Assert
            Assert.IsType<ActionResult<ProfileItem>>(result);
            Assert.Equal(6, profile.Value.Items.Count);
        }

        [Fact]
        public async Task AddProfileItemBadRequestTest()
        {
            // Arrange
            var controller = new ProfileController(_service);
            var item = new ProfileItem() { Id = 1, Type = ProfileItemType.Header, Text = "Accomplishments" };

            // Act
            var result = await controller.AddProfileItem(item);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProfileItemTest()
        {
            // Arrange
            var controller = new ProfileController(_service);
            var item = new ProfileItem() { Id = 1, Type = ProfileItemType.Name, Text = "Joe Coder" };

            // Act
            var result = await controller.DeleteProfileItem(item);
            var profile = await controller.GetProfile();

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(4, profile.Value.Items.Count);
        }

        [Fact]
        public async Task DeleteProfileItemNotFoundTest()
        {
            // Arrange
            var controller = new ProfileController(_service);
            var item = new ProfileItem() { Id = 99, Type = ProfileItemType.Skill, Text = "Does not exist" };

            // Act
            var result = await controller.DeleteProfileItem(item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
