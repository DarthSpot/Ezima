using Ezima.API.Model.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ezima.API.Authentication;

public class ConfigureJwtBearerOptions(IOptions<JwtOptions> options, SecurityKeyHelper securityKeyHelper)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options = options.Value;

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new RsaSecurityKey(securityKeyHelper.PublicRSA)
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}