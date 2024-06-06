using FluentAssertions;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Credentials.Models;
using Tweetinvi.Models;
using Tweetinvi.Security.System.Security.Cryptography;
using Tweetinvi.WebLogic;

namespace MiggraTweets.Tests
{
    public class SignatureGeneratorTests
    {
        /*
         * OAuth oauth_consumer_key="yjghw0vfMh48UubUNksFyI3w2",oauth_nonce="3323339",oauth_signature_method="HMAC-SHA1",oauth_timestamp="1716923527",oauth_version="1.0",oauth_signature="lX0N28DdOfVT9gyNvFPRjZdNrNc%3D"
         */
        [Theory]
        [InlineData("yjghw0vfMh48UubUNksFyI3w2", 3323339, 1716923527, "lX0N28DdOfVT9gyNvFPRjZdNrNc%3D")]
        public void GenerateSignature_ShouldCreateSignature(string oauthConsumerKey, long oauthNonce, long oauthTimestamp, string expectedSignature)
        {
            // Create parameter string

            //var oauthNonce = new Random().Next(123400, 9999999).ToString(CultureInfo.InvariantCulture);

            //var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //var oauthTimestamp = Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            var oauthCallback = "oob";

            var parameters =
                new Dictionary<string, string>()
                {

                };

            var authParameterString =
                $"oauth_consumer_key={oauthConsumerKey}" +
                $"&oauth_nonce={oauthNonce}" +
                "&oauth_signature_method=HMAC-SHA1" +
                $"&oauth_timestamp={oauthTimestamp}" +
                "&oauth_version=1.0";

            var encodedAuthParameterString = StringFormater.UrlEncode(authParameterString);

            var requestTokenUri = "https://api.twitter.com/oauth/request_token";
            var signatureBaseString = "POST&" + StringFormater.UrlEncode(requestTokenUri) + "&" + StringFormater.UrlEncode(authParameterString);
            var oauthConsumerSecret = StringFormater.UrlEncode("ENLQVwVKv4rdH0f3kBoTTTVdGFCAa81mpMb6ldrUTpWmV1mUem") + "&";


            var hmacsha1Generator = new HMACSHA1Generator();
            var signature = StringFormater.UrlEncode(Convert.ToBase64String(hmacsha1Generator.ComputeHash(signatureBaseString, oauthConsumerSecret, Encoding.UTF8)));

            signature.Should().Be(expectedSignature);
        }

        [Fact]
        public void GenerateHeaderForGettingRequestTokenUsingtweetInvi()
        {
            var webHelper = new WebHelper();
            var oauthWebRequestGenerator = new OAuthWebRequestGenerator(webHelper, () => DateTime.UtcNow);


            var requestTokenUri = "https://api.twitter.com/oauth/request_token";



            var consumerKey = "yjghw0vfMh48UubUNksFyI3w2";
            var consumerSecret = "ENLQVwVKv4rdH0f3kBoTTTVdGFCAa81mpMb6ldrUTpWmV1mUem";

            var oAuthQueryParameters = 
                oauthWebRequestGenerator.GenerateApplicationParameters(new ReadOnlyConsumerCredentials(consumerKey, consumerSecret), null);

            var oauthHeader = oauthWebRequestGenerator.GenerateAuthorizationHeader(new Uri(requestTokenUri), Tweetinvi.Models.HttpMethod.POST, oAuthQueryParameters);

            oauthHeader.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GenerateAuthHeaderWithRequestTokenUsingtweetInvi()
        {
            var webHelper = new WebHelper();
            var oauthWebRequestGenerator = new OAuthWebRequestGenerator(webHelper, () => DateTime.UtcNow);


            var requestTokenUri = "https://api.twitter.com/oauth/request_token";



            var consumerKey = "yjghw0vfMh48UubUNksFyI3w2";
            var consumerSecret = "ENLQVwVKv4rdH0f3kBoTTTVdGFCAa81mpMb6ldrUTpWmV1mUem";

            var consumerOnlyCredentials = new ConsumerOnlyCredentials(consumerKey, consumerSecret);
            var request = new AuthenticationRequest(consumerOnlyCredentials);
            request.AuthorizationKey = "K-9jwAAAAAABt-ICAAABj7aNnPA";
            request.AuthorizationSecret = "niZWGK62Qj7MmVTZQUWVifiq8y3Ne3Zx";

            var oAuthQueryParameters =
                oauthWebRequestGenerator.GenerateApplicationParameters(new ReadOnlyConsumerCredentials(consumerKey, consumerSecret), request);

            var oauthHeader = oauthWebRequestGenerator.GenerateAuthorizationHeader(new Uri(requestTokenUri), Tweetinvi.Models.HttpMethod.POST, oAuthQueryParameters);

            oauthHeader.Should().NotBeNullOrEmpty();
        }
    }
}