using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json.Linq;

namespace ServerlessCore.Secret
{
    public class SecretManager : ISecretManager
    {
        private SecretDetail _secretDetail;

        public SecretDetail GetSecretDetail()
        {
            if (_secretDetail != null)
                return _secretDetail;

            string secretName = "serverless-core-secret";

            var config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.USWest2 };
            var client = new AmazonSecretsManagerClient(config);

            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            GetSecretValueResponse response = null;
            try
            {
                response = Task.Run(async () => await client.GetSecretValueAsync(request)).Result;
            }
            catch (ResourceNotFoundException)
            {
                Console.WriteLine("The requested secret " + secretName + " was not found");
            }
            catch (InvalidRequestException e)
            {
                Console.WriteLine("The request was invalid due to: " + e.Message);
            }
            catch (InvalidParameterException e)
            {
                Console.WriteLine("The request had invalid params: " + e.Message);
            }

            var token = JToken.Parse(response.SecretString);
            var json = (JObject)JToken.FromObject(token);

            _secretDetail = new SecretDetail()
            {
                AccessKeyId = json.GetValue("aws-access-key-id").ToString(),
                SecretKey = json.GetValue("aws-secret-key").ToString(),
                UserPoolId = json.GetValue("user-pool-id").ToString(),
                AppClientId = json.GetValue("app-client-id").ToString()
            };

            return _secretDetail;
        }
    }
}
