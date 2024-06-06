using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Authorization.OAuth2;

public struct AuthorizeRequest
{
    public string Url { get; set; }
    public string State { get; set; }
    public string CodeChallenge { get; set; }
}
