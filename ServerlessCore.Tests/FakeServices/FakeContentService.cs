using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Threading.Tasks;

namespace ServerlessCore.Tests.FakeServices
{
    public class FakeContentService : IContentService
    {
        private readonly ContentDictionary _content;

        public FakeContentService()
        {
            _content = new ContentDictionary
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" }
            };
        }

        public async Task AddContentItemAsync(ContentItem contentItem)
        {
            await Task.Run(() => _content.Add(contentItem.Name, contentItem.Value));
        }

        public async Task DeleteContentItemAsync(ContentItem contentItem)
        {
            await Task.Run(() => _content.Remove(contentItem.Name));
        }

        public async Task<ContentDictionary> GetContentAsync()
        {
            return await _content;
        }

        public async Task<ContentItem> GetContentItemAsync(string name)
        {
            string value;
            _content.TryGetValue(name, out value);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            var result = new ContentItem() { Name = name, Value = value };
            return await Task.FromResult(result);
        }
    }
}
