using Ezima.API.Model.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ezima.API.Authentication;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;
    private readonly SecurityKeyHelper _securityKeyHelper;

    public ConfigureJwtBearerOptions(JwtOptions options, SecurityKeyHelper securityKeyHelper)
    {
        _options = options;
        _securityKeyHelper = securityKeyHelper;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new RsaSecurityKey(_securityKeyHelper.PublicRSA)
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}