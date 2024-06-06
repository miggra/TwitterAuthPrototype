namespace MiggraTweets.UseCases.Tweets.Post.PostTweetCommandArguments;

public class Poll
{
    public int DurationMinutes { get; set; }
    public string[] Options { get; set; } = null!;
}