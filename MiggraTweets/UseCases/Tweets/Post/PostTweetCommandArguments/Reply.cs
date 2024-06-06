namespace MiggraTweets.UseCases.Tweets.Post.PostTweetCommandArguments;

public class Reply
{
    public string[] ExcludeReplyUserIds { get; set; } = null!;

    public string[] InReplyToTweetId { get; set; } = null!;
}