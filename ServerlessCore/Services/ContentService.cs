using System.Linq;
using System.Threading.Tasks;
using ServerlessCore.Data.Models;
using ServerlessCore.Libs.Mappers;
using ServerlessCore.Libs.Repositories;

namespace ServerlessCore.Services
{
    public class ContentService : IContentService
    {
        private readonly ContentDictionary _content = new ContentDictionary();
        private readonly IContentRepository _repo;
        private readonly IMapper _mapper;

        public ContentService(IContentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ContentDictionary> GetContentAsync()
        {
            var response = await _repo.GetAllItems();
            var items = _mapper.ToContentItem(response).ToList();
            var content = new ContentDictionary();

            items.ForEach(c => content.Add(c.Name, c.Value));

            return content;
        }

        public async Task<ContentItem> GetContentItemAsync(string name)
        {
            var response = await _repo.GetItem(name);
            if (response == null)
                return null;

            return _mapper.ToContentItem(response);
        }

        public async Task AddContentItemAsync(ContentItem contentItem)
        {
            await _repo.AddItem(_mapper.ToContentData(contentItem));
        }

        public async Task DeleteContentItemAsync(ContentItem contentItem)
        {
            await _repo.DeleteItem(_mapper.ToContentData(contentItem));
        }
    }
}