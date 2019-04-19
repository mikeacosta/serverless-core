using System.Linq;
using System.Threading.Tasks;
using ServerlessCore.Data.Models;
using ServerlessCore.Libs.Mappers;
using ServerlessCore.Libs.Repositories;

namespace ServerlessCore.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repo;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CollectionDto<ProfileItem>> GetProfileAsync()
        {
            var response = await _repo.GetAllItems();
            var items = _mapper.ToProfileItem(response).OrderBy(j => j.Id).ToList();
            return new CollectionDto<ProfileItem> { Items = items };

        }

        public async Task<ProfileItem> GetProfileItemAsync(int id)
        {
            var response = await _repo.GetItem(id);
            if (response == null)
                return null;

            return _mapper.ToProfileItem(response);
        }

        public async Task AddProfileItemAsync(ProfileItem profileItem)
        {
            await _repo.AddItem(_mapper.ToProfileData(profileItem));
        }

        public async Task DeleteProfileItemAsync(ProfileItem profileItem)
        {
            await _repo.DeleteItem(_mapper.ToProfileData(profileItem));
        }
    }
}
