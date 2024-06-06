namespace MiggraTweets.Authorization.OAuth2;

public static class ScopesListExtensions
{
    public static string ToScopesString(this List<string> strings)
    {
        return string.Join("%20", strings);
    }
}
