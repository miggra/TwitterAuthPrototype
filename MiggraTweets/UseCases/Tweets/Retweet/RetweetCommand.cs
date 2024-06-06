using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.UseCases.Tweets.Retweet;

public class RetweetCommand
{
    /// <summary>
    /// The user ID who you are Retweeting a Tweet on behalf of. 
    /// It must match your own user ID or that of an authenticating user, 
    /// meaning that you must pass the Access Tokens 
    /// associated with the user ID when authenticating your request.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// The ID of the Tweet that you would like the user id to Retweet.
    /// </summary>
    public string TweetId { get; set; } = null!;
}
