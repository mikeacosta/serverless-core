using ServerlessCore.Data.Models;
using ServerlessCore.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessCore.Tests.FakeServices
{
    public class FakeProfileService : IProfileService
    {
        private readonly CollectionDto<ProfileItem> _profile;

        public FakeProfileService()
        {
            _profile = new CollectionDto<ProfileItem>()
            {
                Items = new List<ProfileItem>()
                {
                    new ProfileItem() { Id = 1, Type = ProfileItemType.Name, Text = "Joe Coder" },
                    new ProfileItem() { Id = 2, Type = ProfileItemType.Summary, Text = "An experienced developer." },
                    new ProfileItem() { Id = 3, Type = ProfileItemType.Header, Text = "Accomplishments" },
                    new ProfileItem() { Id = 4, Type = ProfileItemType.Highlight, Text = "Built a web app." },
                    new ProfileItem() { Id = 5, Type = ProfileItemType.Highlight, Text = "Drove a forklift." }
                }
            };
        }

        public async Task AddProfileItemAsync(ProfileItem profileItem)
        {
            await Task.Run(() => _profile.Items.Add(profileItem));
        }

        public async Task DeleteProfileItemAsync(ProfileItem profileItem)
        {
            await Task.Run(() => _profile.Items.Remove(profileItem));
        }

        public async Task<CollectionDto<ProfileItem>> GetProfileAsync()
        {
            return await _profile;
        }

        public async Task<ProfileItem> GetProfileItemAsync(int id)
        {
            var item = _profile.Items.FirstOrDefault(p => p.Id == id);

            return await Task.FromResult(item);
        }
    }
}
