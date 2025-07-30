using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Barbearia.Infrastructure.Security.Token;

public class JwtTokenGenerator : IAcessTokenGenerator
{
    private readonly uint _expirationInMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationInMinutes, string signingKey)
    {
        _expirationInMinutes = expirationInMinutes;
        _signingKey = signingKey;
    }

    public string Generate(Usuario usuario)
    {

        var Claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Sid, usuario.UserIdentifier.ToString()),
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(),  SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(Claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);
        
        return new SymmetricSecurityKey(key);
    }
}