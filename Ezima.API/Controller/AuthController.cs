using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AspNet.Security.OAuth.Discord;
using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Config;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ezima.API.Controller;

[ApiController]
[Route("/api/auth")]
public class AuthController(SecurityKeyHelper helper, UserRepository userRepository) : ControllerBase
{
    [HttpGet]
    [Jauthorize]
    public IActionResult GetAuthInfo()
    {
        var accessToken = HttpContext.User.Claims;
        var data = accessToken.Select(x => new { x.Type, x.Value }).ToList();
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpGet("/login")]
    public IResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/get-token",
            IsPersistent = true
        };

        return Results.Challenge(properties, [DiscordAuthenticationDefaults.AuthenticationScheme]);
    }

    [HttpGet("/.well-known/jwks.json")]
    public IActionResult GetJwt()
    {
        Console.WriteLine("Serving JWKs");

        var rsaKey = helper.PublicRSA;
        var rsaParameters = rsaKey.ExportParameters(false);

        var jwk = new JsonWebKey
        {
            Kty = "RSA",
            E = Base64UrlEncoder.Encode(rsaParameters.Exponent),
            N = Base64UrlEncoder.Encode(rsaParameters.Modulus),
            Kid = "vasitos-public-key",
            Use = "sig",
            KeyOps = { "verify" },
            Alg = SecurityAlgorithms.RsaSha256
        };

        var jwks = new
        {
            Keys = new[] { jwk }
        };

        return Ok(jwks);
    }

    [HttpGet("/get-token")]
    public async Task<IActionResult> GetToken()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded) 
            return Unauthorized();

        var claims = result.Principal.Claims.ToList();
        var permissions = new List<string>
        {
            "user-add",
            "user-view"
        };
        
        var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        var userMail = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        if (userId is not null && userName is not null && userMail is not null)
        {
            var user = await userRepository.FindByOAuthId(userId);
            if (user != null)
            {
                // Login
                user.LastActive = DateTime.UtcNow;
                await userRepository.Save(user);
            }
            else
            {
                user = new User()
                {
                    Username = userName,
                    OAuthId = userId,
                    EMail = userMail,
                    Created = DateTime.UtcNow,
                    LastActive = DateTime.UtcNow,
                    FullName = userName,
                };
                user = await userRepository.Save(user);
                claims.Add(new Claim("EzimaUserId", user.Id.ToString()));
            }
        }

        claims.AddRange(permissions.Select(permission => new Claim("permissions", permission)));
    
        // Create JWT token
        var tokenString = EncryptJwtToken(claims);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { token = tokenString });
    }

    private string EncryptJwtToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var rsa = helper.PrivateRSA;
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