using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.UseCases.Tweets.Retweet;

public class RetweetCommandResponse
{
    public bool Retweeted { get; }

    public RetweetCommandResponse(bool retweeted)
    {
        Retweeted = retweeted;
    }
}