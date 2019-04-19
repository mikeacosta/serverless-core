using ServerlessCore.Data.Models;
using System.Threading.Tasks;

namespace ServerlessCore.Services
{
    public interface IProfileService
    {
        Task<CollectionDto<ProfileItem>> GetProfileAsync();

        Task<ProfileItem> GetProfileItemAsync(int id);

        Task AddProfileItemAsync(ProfileItem profileItem);

        Task DeleteProfileItemAsync(ProfileItem profileItem);
    }
}
