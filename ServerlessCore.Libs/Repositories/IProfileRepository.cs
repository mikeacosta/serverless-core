using ServerlessCore.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessCore.Libs.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<ProfileData>> GetAllItems();

        Task<ProfileData> GetItem(int Id);

        Task AddItem(ProfileData item);

        Task DeleteItem(ProfileData item);
    }
}
