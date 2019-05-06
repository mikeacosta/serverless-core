using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using ServerlessCore.Secret;
using System.Threading.Tasks;

namespace ServerlessCore.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SecretDetail _secretDetail;

        public class AppUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        public AuthenticationController(ISecretManager secretManager)
        {
            _secretDetail = secretManager.GetSecretDetail();
        }

        //[HttpPost]
        //[Route("api/register")]
        //public async Task<ActionResult<string>> Register(AppUser user)
        //{
        //    var cognito = new AmazonCognitoIdentityProviderClient(RegionEndpoint.USWest2);

        //    var request = new SignUpRequest
        //    {
        //        ClientId = _clientId,
        //        Password = user.Password,
        //        Username = user.Username
        //    };

        //    var emailAttribute = new AttributeType
        //    {
        //        Name = "email",
        //        Value = user.Email
        //    };
        //    request.UserAttributes.Add(emailAttribute);

        //    var response = await cognito.SignUpAsync(request);

        //    return Ok();
        //}

        [HttpPost]
        [Route("api/signin")]
        public async Task<ActionResult<string>> SignIn(AppUser user)
        {
            var cognito = new AmazonCognitoIdentityProviderClient(new Creds(_secretDetail));

            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = _secretDetail.UserPoolId,
                ClientId = _secretDetail.AppClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };

            request.AuthParameters.Add("USERNAME", user.Username);
            request.AuthParameters.Add("PASSWORD", user.Password);

            var response = await cognito.AdminInitiateAuthAsync(request);

            return Ok(response.AuthenticationResult.IdToken);
        }

        public class Creds : AWSCredentials
        {
            private readonly SecretDetail _secretDetail;

            public Creds(SecretDetail secretDetail)
            {
                _secretDetail = secretDetail;
            }

            public override ImmutableCredentials GetCredentials()
            {
                return new ImmutableCredentials(_secretDetail.AccessKeyId, _secretDetail.SecretKey, string.Empty);
            }
        }
    }
}
