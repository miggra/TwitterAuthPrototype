namespace MiggraTweets.Helpers;

internal static class StringExtensions
{
    internal static string JoinToString<T>(this IEnumerable<T> source, string separator)
    {
        return string.Join(separator, source);
    }
}
