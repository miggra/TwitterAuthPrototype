using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.UseCases.Likes.LikeTweet;

public class LikeTweetResponse
{
    public bool Liked { get; }

    public LikeTweetResponse(bool liked)
    {
        Liked = liked;
    }
}
