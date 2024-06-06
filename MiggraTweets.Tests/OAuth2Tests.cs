using FluentAssertions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MiggraTweets.Authorization.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Models;

namespace MiggraTweets.Tests;

public class OAuth2Tests
{
    [Fact]
    public void Proof_ShouldCreateExpectedUrl()
    {
        var oauth = new OAuth2();

        var url = oauth.ConstructAuthorizeUrl(
            "WDd3VFZlY1l4bnpPWExhelRYUkw6MTpjaQ",
            "http://127.0.0.1:5001/sign-in",
            new Scopes { "tweet.read", "tweet.write", "users.read", "like.write" });

        url.Should();
    }
}
