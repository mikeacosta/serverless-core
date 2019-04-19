using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServerlessCore.Data.Models
{
    public class ProfileItem
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProfileItemType Type { get; set; }

        public string Text { get; set; }
    }
}
