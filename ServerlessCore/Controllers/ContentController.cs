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
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContentDictionary>> GetContent()
        {
            var content = await _contentService.GetContentAsync();

            return content;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContentItem>> AddContentItem([FromBody] ContentItem contentItem)
        {
            var item = await _contentService.GetContentItemAsync(contentItem.Name);
            if (item != null)
                return BadRequest();

            await _contentService.AddContentItemAsync(contentItem);
            return CreatedAtAction(nameof(AddContentItem), contentItem);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContentItem([FromBody] ContentItem contentItem)
        {
            var item = await _contentService.GetContentItemAsync(contentItem.Name);
            if (item == null)
                return NotFound();

            await _contentService.DeleteContentItemAsync(contentItem);
            return Ok();
        }
    }
}