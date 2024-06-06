using Microsoft.AspNetCore.Mvc;
using MiggraTweets.Authorization.OAuth1.RequestToken;
using MiggraTweets.Authorization.OAuth2;
using MiggraTweets.UseCases.Tweets.Post;
using TwitterAppPrototype.Api.Configurations.Options;
using TwitterAppPrototype.Api.UseCases.Twitter;
using TwitterAppPrototype.Api.UseCases.Twitter.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.Configure<TwitterAppAccessOptions>(
    builder.Configuration.GetSection(nameof(TwitterAppAccessOptions)));
builder.Services.Configure<TwitterRequredScopes>(options => 
    builder.Configuration.GetSection(nameof(TwitterRequredScopes)).Bind(options));

builder.Services.AddSingleton<IAuthorizer, Authorizer>();
builder.Services.AddSingleton<GetAccessTokenHandler>();
builder.Services.AddSingleton<TwitterActions>();
builder.Services.AddSingleton<PostTweetCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapGet("/oauth2/authorize/twitter", (IAuthorizer authorizer) => 
    {
        var redirectUrl = authorizer.CreateAuthorizeUrl();
        return Results.Redirect(redirectUrl);
    });

//app.MapPost("/oauth1/request-x-token", async (RequestTokenHandler handler) =>
//    {
//        return await handler.RequestToken();
//    }
//);

app.MapGet("/twitter/sign-in", (
    [FromQuery(Name = "state")] string state,
    [FromQuery(Name = "code")] string code) =>
    {
        return code;
    });

app.MapPost("/oauth2/authorize/twitter/access-token", async (
    [FromQuery(Name = "code")] string code, IAuthorizer authorizer) =>
    {
        return await authorizer.CreateTokensAsync(code);
    });

app.MapPost("/oauth2/authorize/twitter/refresh-access-token", async (
    [FromQuery(Name = "refresh_token")] string refreshToken, IAuthorizer authorizer) =>
{
    return await authorizer.RefreshTokenAsync(refreshToken);
});

app.MapPost("/referal-tasks/twitter/post-tweet", async (
    [FromQuery(Name = "text")] string text, 
    [FromQuery(Name = "accessToken")] string accessToken, 
    TwitterActions twitterActions) =>
{
    return await twitterActions.PostTweet(text, accessToken);
});

app.Run();
