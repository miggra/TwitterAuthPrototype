using IdentityServer4.Models;
using MiggraTweets.Helpers;
using MiggraTweets.Http;
using System.Text;
using System.Text.Json;

namespace MiggraTweets.Authorization.OAuth2;

public class GetAccessTokenHandler
{
    private const string ACCESS_TOKEN_URI = "https://api.twitter.com/2/oauth2/token";
    private readonly IHttpClientFactory httpClientFactory;

    public GetAccessTokenHandler(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<TokenPair> CreateAccessTokenAsync(string authorizationCode, string clientId, string clientSecret, string redirectUrl)
    {
        var formData = new Dictionary<string, string>
        {
            { "code", authorizationCode },
            { "grant_type", "authorization_code" },
            { "client_id", clientId },
            { "redirect_uri", redirectUrl },
            { "code_verifier", "WDd3VFZlY1l4bnpPWE"},
        };
        using var request = new HttpRequestMessage(HttpMethod.Post, ACCESS_TOKEN_URI) { Content = new FormUrlEncodedContent(formData) };

        var basicAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Add("Authorization", $"Basic {basicAuthHeader}");

        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var tokens = await HttpResponseJsonDeserializer.Deserialize<TokenPair>(response);

        return tokens;
    }

    public async Task<TokenPair> RefreshAccessTokenAsync(string clientId, string clientSecret, string refreshToken)
    {
        var formData = new Dictionary<string, string>
        {
            { "refresh_token", refreshToken },
            { "grant_type", "refresh_token" },
        };
        using var request = new HttpRequestMessage(HttpMethod.Post, ACCESS_TOKEN_URI) { Content = new FormUrlEncodedContent(formData) };

        var basicAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Add("Authorization", $"Basic {basicAuthHeader}");

        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var tokens = await HttpResponseJsonDeserializer.Deserialize<TokenPair>(response);

        return tokens;
    }
}
