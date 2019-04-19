using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;

namespace ServerlessCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CollectionDto<ProfileItem>>> GetProfile()
        {
            var profile = await _profileService.GetProfileAsync();
            return profile;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfileItem>> GetProfileItem(int id)
        {
            var item = await _profileService.GetProfileItemAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProfileItem>> AddProfileItem([FromBody] ProfileItem profileItem)
        {
            var item = await _profileService.GetProfileItemAsync(profileItem.Id);

            if (item != null)
                return BadRequest();

            await _profileService.AddProfileItemAsync(profileItem);
            return CreatedAtAction(nameof(AddProfileItem), profileItem);
        }
   
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProfileItem([FromBody] ProfileItem profileItem)
        {
            var item = await _profileService.GetProfileItemAsync(profileItem.Id);

            if (item == null)
                return NotFound();

            await _profileService.DeleteProfileItemAsync(item);
            return Ok();
        }
    }
}