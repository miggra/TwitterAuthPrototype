using IdentityServer4.Models;
using MiggraTweets.Authorization.OAuth2;
using MiggraTweets.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiggraTweets.UseCases.Tweets.Post;

public class PostTweetCommandHandler
{
    private const string POST_TWEET_URI = "https://api.twitter.com/2/tweets";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly JsonSerializerOptions serializeOptions;

    public PostTweetCommandHandler(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
        this.serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };

    }

    public async Task<PostTweetResult> Handle(PostTweetCommand postTweetCommand, string accessToken)
    {
        using var jsonContent = JsonContent.Create(
            postTweetCommand,
            MediaTypeHeaderValue.Parse("application/json"), 
            this.serializeOptions);

        using var request = new HttpRequestMessage(HttpMethod.Post, POST_TWEET_URI) { Content = jsonContent };

        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var postTweetResult = await HttpResponseJsonDeserializer.Deserialize<PostTweetResult>(response);

        return postTweetResult;
    }
}
