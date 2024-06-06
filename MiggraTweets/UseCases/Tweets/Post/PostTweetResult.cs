using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiggraTweets.UseCases.Tweets.Post.PostTweetResult;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiggraTweets.UseCases.Tweets.Post;

public class PostTweetResult
{
    private DataWrapper Data { get; set; }

    public PostTweetResult(DataWrapper data)
    {
        Data = data;
    }

    public string Id => Data.Id;

    public string Text => Data.Text;

    public class DataWrapper
    {
        public DataWrapper(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; }

        public string Text { get; }
    }
}
