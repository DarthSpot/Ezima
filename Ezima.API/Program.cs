using System.Text;
using Ezima.API.Authentication;
using Ezima.API.Model.Config;
using Ezima.API.Model.Context;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddDbContext<EzimaContext>();
builder.Services.AddTransient<SecurityKeyHelper>();
builder.Services.AddTransient<ChildRepository>();
builder.Services.AddTransient<RewardActivityRepository>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();