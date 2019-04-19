using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Controllers;
using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;
using ServerlessCore.Tests.FakeServices;
using Xunit;

namespace ServerlessCore.Tests
{
    public class ContentControllerTest
    {
        IContentService _service;

        public ContentControllerTest()
        {
            _service = new FakeContentService();
        }

        [Fact]
        public async Task GetContentTest()
        {
            // Arrange
            var controller = new ContentController(_service);

            // Act
            var result = await controller.GetContent();

            // Assert
            Assert.IsType<ActionResult<ContentDictionary>>(result);
            Assert.Equal(3, result.Value.Count);
        }

        [Fact]
        public async Task AddContentItemTest()
        {
            // Arrange
            var controller = new ContentController(_service);
            var item = new ContentItem() { Name = "key4", Value = "value4" };

            // Act
            var result = await controller.AddContentItem(item);
            var content = await controller.GetContent();

            // Assert
            Assert.IsType<ActionResult<ContentItem>>(result);
            Assert.Equal(4, content.Value.Count);
        }

        [Fact]
        public async Task AddContentItemBadRequestTest()
        {
            // Arrange
            var controller = new ContentController(_service);
            var item = new ContentItem() { Name = "key1", Value = "value1" };

            // Act
            var result = await controller.AddContentItem(item);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteContentItemTest()
        {
            // Arrange
            var controller = new ContentController(_service);
            var item = new ContentItem() { Name = "key2", Value = "value2" };

            // Act
            var result = await controller.DeleteContentItem(item);
            var content = await controller.GetContent();

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.Equal(2, content.Value.Count);
        }

        [Fact]
        public async Task DeleteContentItemNotFoundTest()
        {
            // Arrange
            var controller = new ContentController(_service);
            var item = new ContentItem() { Name = "keyNotFound", Value = "valueNotFound" };

            // Act
            var result = await controller.DeleteContentItem(item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
