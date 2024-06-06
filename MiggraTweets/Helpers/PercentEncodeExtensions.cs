using System.Text;

namespace MiggraTweets.Helpers;

public static class PercentEncodeExtensions
{
    public static string ToPercentEncoded(this string str)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".Contains(c.ToString()))
            {
                stringBuilder.Append(c);
            }
            else
            {
                stringBuilder.Append("%" + $"{(int)c:X2}");
            }
        }

        return stringBuilder.ToString();
    }
}
