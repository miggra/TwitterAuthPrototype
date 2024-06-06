using MiggraTweets.Helpers;

namespace MiggraTweets.Authorization.OAuth2;

public static class OAuth2
{
    public static AuthorizeRequest ConstructAuthorizeUrl(string clientId, string redirectUrl, List<string> scopes)
    {
        var parameters = new Dictionary<string, string>()
        {
            ["response_type"] = "code",
            ["client_id"] = clientId,
            ["redirect_uri"] = redirectUrl,
            ["scope"] = scopes.ToScopesString(),
            ["state"] = Guid.NewGuid().ToString(),
            ["code_challenge"] = "WDd3VFZlY1l4bnpPWE",
            ["code_challenge_method"] = "plain",
        };

        var uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "https";
        uriBuilder.Host = "twitter.com";
        uriBuilder.Path = "/i/oauth2/authorize";
        uriBuilder.Query = parameters
            .Select(p => p.Key + "=" + p.Value)
            .JoinToString("&");

        return new AuthorizeRequest { 
            Url = uriBuilder.Uri.ToString(), 
            State = parameters["state"], 
            CodeChallenge = parameters["code_challenge"] };
    }
}
