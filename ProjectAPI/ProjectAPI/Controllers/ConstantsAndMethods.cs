using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectAPI.Data.Models.Exceptions;
using ProjectAPI.Data.Models.View;

namespace ProjectAPI.Controllers;

public class ConstantsAndMethods
{
    public static string EncryptUsingSha256(string rawData)
    {
        // Create a SHA256   
        using SHA256 sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array  
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        // Convert byte array to a string   
        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
    public static Tuple<string, DateTime> GenerateJwtToken(string customerId, string userRole,string email ,string hash)
    {
        DateTime expiry = DateTime.UtcNow.AddDays(7); //TODO Change to whatever rule we have
        var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim("CustomerID", customerId),
                new Claim("UserRole", userRole),
                new Claim("Email",email)
            }),
            Expires = expiry,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(hash))
                , SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return new Tuple<string, DateTime>(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDesc)), expiry);
    }
    public static string GetJwtClaimValue(ClaimsPrincipal user, string type)
    {
        try
        {
            var value = user.Claims.Single(c => c.Type == type).Value;
            return value;
        }
        catch
        {
            if (user == null || user.Claims.All(c => c.Type != type))
            {
                throw new NotLoggedInException();
            }
            throw; 
        }
    }
    public static List<ProductViewModel>? PopulateDatabaseWithProducts()
    {
        var serializer = new JsonSerializer();
        using var sReader = new StreamReader("./Data/Models/TestData/product.json");
        using var tReader = new JsonTextReader(sReader);
        List<ProductViewModel>? products = serializer.Deserialize<List<ProductViewModel>>(tReader);

        return products;
    }
    public static string ConvertNumberToCode(int num)
    {
        StringBuilder result = new StringBuilder();
        var validLetters = "CDFGHJKLMNPQRSTVWXY45679";
        var numberBase = validLetters.Length;
        if (num == 0)
            return validLetters[num].ToString();
        while (num > 0)
        {
            var rem = num % numberBase;
            num /= numberBase;
            result.Insert(0, validLetters[rem]);
        }
        return result.ToString();
    }
}