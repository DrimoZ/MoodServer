using System.Text;

namespace Application.Services.Utils;

public class IdService
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
    private static readonly Random Random = new Random();

    private static string GenerateRandomId(int length)
    {
        var result = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            result.Append(Chars[Random.Next(Chars.Length)]);
        }
        
        
        return result.ToString();
    }

    public static string Generate32CharId()
    {
        return GenerateRandomId(32);
    }
}