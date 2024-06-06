using MiggraTweets.UseCases.Tweets.Post.PostTweetCommandArguments;

namespace MiggraTweets.UseCases.Tweets.Post;

public class PostTweetCommand
{
    public string? Text { get; set; }

    public string? DirectMessageDeepLink { get; set; }
    public bool? ForSuperFollowersOnly { get; set; }
    
    public Geo? Geo { get; set; }

    public Media? Media { get; set; }

    public Poll? Poll {  get; set; }

    public string? QuoteTweetId { get; set; }

    public Reply? Reply { get; set; }

    public ReplySettings? ReplySettings { get; set; }
}
