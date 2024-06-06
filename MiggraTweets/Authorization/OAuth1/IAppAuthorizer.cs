using MiggraTweets.Http;

namespace MiggraTweets.Authorization.OAuth1;

public interface IAppAuthorizer
{
    string CreateHeader(MethodType httpMethod, Uri url, IEnumerable<KeyValuePair<string, string>>? parameters = null, string? token = null, string? tokenSecret = null);
}