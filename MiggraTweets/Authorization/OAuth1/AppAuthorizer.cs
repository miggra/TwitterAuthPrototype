using MiggraTweets.Helpers;
using MiggraTweets.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Tweetinvi.Security.System.Security.Cryptography;

namespace MiggraTweets.Authorization.OAuth1;

public class AppAuthorizer : IAppAuthorizer
{
    private string apiKey;
    private string apiKeySecret;

    public AppAuthorizer(string apiKey, string apiKeySecret)
    {
        this.apiKey = apiKey;
        this.apiKeySecret = apiKeySecret;
    }

    public string CreateHeader(MethodType httpMethod, Uri url, IEnumerable<KeyValuePair<string, string>>? parameters = null, string? token = null, string? tokenSecret = null)
    {
        var operationTime = DateTime.UtcNow;
        var ts = operationTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var oauthTimestamp = Convert.ToInt64(ts.TotalSeconds);
        var oauthNonce = new Random().Next(123400, 9999999);

        var authorizationParameters = GenerateAuthorzationParameters(this.apiKey, oauthTimestamp, oauthNonce, token);
        authorizationParameters.Add(
            "oauth_signature", 
            CreateSignature(this.apiKey, token, this.apiKeySecret, tokenSecret, oauthTimestamp, oauthNonce, httpMethod, url, parameters));
        return "OAuth " + authorizationParameters.Select(p => string.Format(@"{0}=""{1}""", p.Key, p.Value)).JoinToString(",");
    }

    public static string CreateSignature(string consumerKey, string? token, string consumerSecret, string? tokenSecret, long timestamp, int nonce, MethodType httpMethod, Uri url, IEnumerable<KeyValuePair<string, string>>? parameters = null)
    {
        var httpMethodPart = httpMethod.ToString().ToUpperInvariant();

        var urlPart = url.GetComponents(
            UriComponents.Scheme |
            UriComponents.UserInfo |
            UriComponents.Host |
            UriComponents.Port |
            UriComponents.Path,
            UriFormat.UriEscaped);

        var requiredAutorizationParams = GenerateAuthorzationParameters(consumerKey, timestamp, nonce, token);

        var paramsPart = requiredAutorizationParams
            .Concat(parameters ?? new Dictionary<string, string>())
            .Select(x => new KeyValuePair<string, string>(x.Key.ToPercentEncoded(), x.Value.ToPercentEncoded()))
            .Concat(
                url.GetQueryParams() // already percent encoded
            )
            .OrderBy(x => x.Key).ThenBy(x => x.Value)
            .Select(x => x.Key + "=" + x.Value)
            .JoinToString("&");


        var signatureBaseString = $"{httpMethodPart}&{urlPart.ToPercentEncoded()}&{paramsPart.ToPercentEncoded()}";
        var signinKey = $"{consumerSecret.ToPercentEncoded()}&{tokenSecret?.ToPercentEncoded()}";

        var hmacsha1Generator = new HMACSHA1Generator();
        var signature = Convert.ToBase64String(hmacsha1Generator.ComputeHash(signatureBaseString, signinKey, Encoding.UTF8)).ToPercentEncoded();

        return signature;
    }

    private static Dictionary<string, string> GenerateAuthorzationParameters(string consumerKey, long timestamp, int nonce, string? token = null)
    {
        var requiredAutorizationParams = GenerateAlwaysRequiredAutorizationAdditionalParams(timestamp, nonce);

        requiredAutorizationParams.Add("oauth_consumer_key", consumerKey);

        if (!string.IsNullOrEmpty(token))
            requiredAutorizationParams.Add("oauth_token", token);

        return requiredAutorizationParams;
    }

    public static Dictionary<string, string> GenerateAlwaysRequiredAutorizationAdditionalParams(long timestamp, int nonce)
    {
        return new Dictionary<string, string>
        {
            ["oauth_nonce"] = nonce.ToString(CultureInfo.InvariantCulture),
            ["oauth_signature_method"] = "HMAC-SHA1",
            ["oauth_timestamp"] = timestamp.ToString(CultureInfo.InvariantCulture),
            ["oauth_version"] = "1.0",
        };
    }
}
