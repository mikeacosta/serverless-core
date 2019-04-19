using Amazon.DynamoDBv2.DataModel;

namespace ServerlessCore.Libs.Models
{
    [DynamoDBTable("Content")]
    public class ContentData
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
