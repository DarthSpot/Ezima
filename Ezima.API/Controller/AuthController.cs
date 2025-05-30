using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AspNet.Security.OAuth.Discord;
using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpGet]
    public IActionResult GetUserInfo()
    {
        var accessToken = HttpContext.User.Claims;

        return Ok();
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

        var rsaKey = _helper.PublicRSA;
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

        if (!result.Succeeded) return Unauthorized();

        var claims = result.Principal.Claims.ToList();
        var permissions = new List<string>
        {
            "user-add",
            "user-view"
        };

        claims.AddRange(permissions.Select(permission => new Claim("permissions", permission)));
    
        // Create JWT token
        var tokenString = EncryptJwtToken(claims);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { token = tokenString });
    }

    private string EncryptJwtToken(List<Claim> claims)
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