using FluentAssertions;
using MiggraTweets.Authorization.OAuth1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Tests;

public class AppAuthorizerTests
{
    [Theory]
    [InlineData("yjghw0vfMh48UubUNksFyI3w2", "ENLQVwVKv4rdH0f3kBoTTTVdGFCAa81mpMb6ldrUTpWmV1mUem", 1716941230, 8285848, "Ievnqdlw995yuPVNokHPiWguMcA%3D")]
    public async Task ShouldCreateExpectedSignature(string consumerKey, string consumerSecret, long timestamp, int nonce, string expectedSignature)
    {
        // Act
        var signature = AppAuthorizer.CreateSignature(consumerKey, null, consumerSecret, null, timestamp, nonce, Http.MethodType.Post, new Uri("https://api.twitter.com/oauth/request_token"));


        // Assert
        signature.Should().Be(expectedSignature);
    }
}
