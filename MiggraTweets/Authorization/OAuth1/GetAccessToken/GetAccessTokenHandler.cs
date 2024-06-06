using MiggraTweets.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Authorization.OAuth1.GetAccessToken;

public class GetAccessTokenHandler
{
    private const string ACCESS_TOKEN_URI = "https://api.twitter.com/oauth/access_token";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IAppAuthorizer appAuthorizer;

    public GetAccessTokenHandler(IHttpClientFactory httpClientFactory, IAppAuthorizer appAuthorizer)
    {
        this.httpClientFactory = httpClientFactory;
        this.appAuthorizer = appAuthorizer;
    }

    public async Task<string> CreateAccessToken(string requestToken, string verifier)
    {
        var authorizationHeader = this.appAuthorizer.CreateHeader(
            MethodType.Post,
            new Uri(ACCESS_TOKEN_URI),
            null, 
            requestToken);

        var request = new HttpRequestMessage(HttpMethod.Post, ACCESS_TOKEN_URI);

        request.Headers.Add("Authorization", authorizationHeader);

        var httpClient = this.httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
