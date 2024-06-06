namespace MiggraTweets.UseCases.Tweets.Post.PostTweetCommandArguments;

public class Media
{
    public string[] MediaIds { get; set; } = null!;

    public string[] TaggedUserIds { get; set; } = null!;
}