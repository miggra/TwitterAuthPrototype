using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiggraTweets.Helpers;

public static class Base64Helper
{
    public static string GetRandomBase64()
    {
        var randomDigits = new Random().Next(123400, 9999999);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(randomDigits.ToString()));
    }

    public static string ToBase64(this string str)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }
}
