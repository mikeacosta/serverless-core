using Amazon.DynamoDBv2.DataModel;
using ServerlessCore.Data.Models;

namespace ServerlessCore.Libs.Models
{
    [DynamoDBTable("Profile")]
    public class ProfileData
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        public ProfileItemType Type { get; set; }

        public string Text { get; set; }
    }
}
