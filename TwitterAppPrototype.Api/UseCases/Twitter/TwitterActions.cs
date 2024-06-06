using MiggraTweets.UseCases.Tweets.Post;

namespace TwitterAppPrototype.Api.UseCases.Twitter
{
    public class TwitterActions
    {
        private readonly PostTweetCommandHandler postTweetCommandHandler;

        public TwitterActions(
            PostTweetCommandHandler postTweetCommandHandler)
        {
            this.postTweetCommandHandler = postTweetCommandHandler;
        }


        public async Task<string> PostTweet(string text, string accessToken)
        {
            var command = new PostTweetCommand() { Text = text };

            var result = await this.postTweetCommandHandler.Handle(command, accessToken);

            return result.Id;
        }
    }
}
