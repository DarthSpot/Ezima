using System.Globalization;
using AspNet.Security.OAuth.Discord;
using Ezima.API.Model.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Ezima.API.Authentication;

public class ConfigureDiscordOptions(IOptions<OAuthSettings> settings)
    : IConfigureNamedOptions<DiscordAuthenticationOptions>
{
    private readonly OAuthSettings _settings = settings.Value;

    public void Configure(DiscordAuthenticationOptions options)
    {
        options.ClientId = _settings.ClientId;
        options.ClientSecret = _settings.ClientSecret;
        options.CallbackPath = new PathString("/signin-discord");
        options.SaveTokens = true;

        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;

        options.ClaimActions.MapCustomJson("urn:discord:avatar:url", user =>
            string.Format(
                CultureInfo.InvariantCulture,
                "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                user.GetString("id"),
                user.GetString("avatar"),
                user.GetString("avatar")!.StartsWith("a_") ? "gif" : "png"));

        options.Scope.Add("identify");
        options.Scope.Add("email");
    }

    public void Configure(string? name, DiscordAuthenticationOptions options)
    {
        Configure(options);
    }
}