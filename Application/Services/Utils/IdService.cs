using System.Text;

namespace Application.Services.Utils;

public class IdService
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
    private readonly Random _random = new Random();
    
    public string GenerateRandomId(int length)
    {
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(Chars[_random.Next(Chars.Length)]);
        }
        
        
        return result.ToString();
    }
}