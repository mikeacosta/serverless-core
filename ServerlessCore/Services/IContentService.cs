using ServerlessCore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessCore.Services
{
    public interface IContentService
    {
        Task<ContentDictionary> GetContentAsync();

        Task<ContentItem> GetContentItemAsync(string name);

        Task AddContentItemAsync(ContentItem contentItem);

        Task DeleteContentItemAsync(ContentItem contentItem);
    }
}
