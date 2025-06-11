using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Ezima.API.Authentication;

public class JWTauthorizeAttribute : AuthorizeAttribute
{
    public JWTauthorizeAttribute() : base()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}