using ServerlessCore.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessCore.Libs.Repositories
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentData>> GetAllItems();

        Task<ContentData> GetItem(string name);

        Task AddItem(ContentData item);

        Task DeleteItem(ContentData item);
    }
}
