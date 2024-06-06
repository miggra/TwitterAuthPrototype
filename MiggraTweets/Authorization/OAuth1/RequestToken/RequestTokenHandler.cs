using MiggraTweets.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Authorization.OAuth1.RequestToken;

public class RequestTokenHandler
{
    private const string REQUEST_TOKEN_URI = "https://api.twitter.com/oauth/request_token";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IAppAuthorizer appAuthorizer;

    public RequestTokenHandler(IHttpClientFactory httpClientFactory, IAppAuthorizer appAuthorizer)
    {
        this.httpClientFactory = httpClientFactory;
        this.appAuthorizer = appAuthorizer;
    }

    public async Task<string> RequestToken()
    {
        var authorizationHeader = this.appAuthorizer.CreateHeader(
            MethodType.Post,
            new Uri(REQUEST_TOKEN_URI));
        
        var request = new HttpRequestMessage(HttpMethod.Post, REQUEST_TOKEN_URI);

        request.Headers.Add("Authorization", authorizationHeader);

        var httpClient = this.httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
