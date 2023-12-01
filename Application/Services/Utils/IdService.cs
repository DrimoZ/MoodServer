using System.Text;

namespace Application.Services.Utils;

public class IdService
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
    private static readonly Random Random = new Random();

    private static string GenerateRandomId(int length, EClassType ct)
    {
        length = length - 4;
        
        var result = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            result.Append(Chars[Random.Next(Chars.Length)]);
        }

        return ct switch
        {
            EClassType.User => "usr_" + result.ToString(),
            EClassType.Account => "acc_" + result.ToString(),
            _ => throw new KeyNotFoundException("Unknown Class Type")
        };
    }

    public static string Generate32CharId(EClassType ct)
    {
        return GenerateRandomId(32, ct);
    }
}