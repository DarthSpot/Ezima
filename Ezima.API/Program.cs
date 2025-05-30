using System.Globalization;
using System.Text;
using AspNet.Security.OAuth.Discord;
using Ezima.API.Authentication;
using Ezima.API.Model.Config;
using Ezima.API.Model.Context;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<OAuthSettings>(builder.Configuration.GetSection("OAuthSettings"));
builder.Services.AddDbContext<EzimaContext>();
builder.Services.AddTransient<ChildRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<RewardActivityRepository>();
builder.Services.AddTransient<SecurityKeyHelper>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme)
    .AddDiscord()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "DiscordAuth";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
builder.Services.ConfigureOptions<ConfigureDiscordOptions>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();