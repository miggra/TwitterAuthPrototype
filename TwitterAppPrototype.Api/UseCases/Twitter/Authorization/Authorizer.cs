using Microsoft.Extensions.Options;
using MiggraTweets.Authorization.OAuth2;
using System.Text.Json;
using TwitterAppPrototype.Api.Configurations.Options;
using GetAccessTokenHandler = MiggraTweets.Authorization.OAuth2.GetAccessTokenHandler;

namespace TwitterAppPrototype.Api.UseCases.Twitter.Authorization;

public class Authorizer : IAuthorizer
{
    private TwitterAppAccessOptions accessOptions;
    private readonly TwitterRequredScopes scopesOptions;
    private readonly GetAccessTokenHandler getAccessTokenHandler;

    public Authorizer(IOptions<TwitterAppAccessOptions> accessOptions,
        IOptions<TwitterRequredScopes> scopesOptions,
        GetAccessTokenHandler getAccessTokenHandler)
    {
        this.accessOptions = accessOptions.Value ?? throw new ArgumentNullException("accessOptions");
        this.scopesOptions = scopesOptions.Value ?? throw new ArgumentNullException("scopesOptions");
        this.getAccessTokenHandler = getAccessTokenHandler;
    }

    public string CreateAuthorizeUrl()
    {
        var authorizationRequest = OAuth2.ConstructAuthorizeUrl(accessOptions.ClientId!, accessOptions.RedirectUrl!, scopesOptions.Scopes!);
        return authorizationRequest.Url;
    }

    public async Task<string> CreateTokensAsync(string authorizationCode)
    {
        var tokenPair = await getAccessTokenHandler.CreateAccessTokenAsync(
            authorizationCode, accessOptions.ClientId!, accessOptions.ClientSecret!, accessOptions.RedirectUrl!);
        return JsonSerializer.Serialize(tokenPair);
    }

    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var tokenPair = await getAccessTokenHandler.RefreshAccessTokenAsync(
            accessOptions.ClientId!, accessOptions.ClientSecret!, refreshToken);
        return JsonSerializer.Serialize(tokenPair);
    }
}
