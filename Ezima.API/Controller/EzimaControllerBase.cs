using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

public abstract class EzimaControllerBase(IAuthScopeService scopeService) : ControllerBase
{
    
}