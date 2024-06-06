using MiggraTweets.Helpers;

namespace MiggraTweets.UseCases.Tweets.Post.PostTweetCommandArguments;

public class ReplySettings : Enumeration
{
    public static ReplySettings MentionedUsers => new(1, "mentionedUsers");
    public static ReplySettings Following => new(2, "following");

    public ReplySettings(int id, string name)
        : base(id, name)
    {
    }
}