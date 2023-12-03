using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Models;

namespace PDP_Students.UI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly ITokenServise _tokenServise;


    public IdentityController(ITokenServise tokenServise)
    {
        _tokenServise = tokenServise;
    }

    
}
