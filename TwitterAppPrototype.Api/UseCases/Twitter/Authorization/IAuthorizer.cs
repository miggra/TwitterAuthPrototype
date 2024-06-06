namespace TwitterAppPrototype.Api.UseCases.Twitter.Authorization;

public interface IAuthorizer
{
    string CreateAuthorizeUrl();
    Task<string> CreateTokensAsync(string authorizationCode);
    Task<string> RefreshTokenAsync(string refreshToken);
}
