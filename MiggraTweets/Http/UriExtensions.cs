using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Http;

internal static class UriExtensions
{
    internal static IEnumerable<KeyValuePair<string, string>> GetQueryParams(this Uri uri)
    {
        return uri.Query.TrimStart('?').Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x =>
            {
                var s = x.Split('=');
                return new KeyValuePair<string, string>(s[0], s[1]);
            });
    }
}
