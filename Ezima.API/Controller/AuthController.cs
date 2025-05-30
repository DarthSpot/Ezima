using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ezima.API.Controller;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly SecurityKeyHelper _helper;

    public AuthController(SecurityKeyHelper helper)
    {
        _helper = helper;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        return Unauthorized();
    }

    private string GenerateJwtToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var rsa = _helper.PrivateRSA;
        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}