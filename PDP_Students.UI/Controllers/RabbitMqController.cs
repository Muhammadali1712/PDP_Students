using Microsoft.AspNetCore.Mvc;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Models;

namespace PDP_Students.UI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RabbitMqController : ControllerBase
{
    private readonly IRabbitMqServise _rabbitMqServise;

    public RabbitMqController(IRabbitMqServise rabbitMqServise)
    {
        _rabbitMqServise = rabbitMqServise;
    }
    [HttpGet]
    public async Task<bool> Send(string massage)
    {
        QueryModel queryModel = new QueryModel()
        {
            Hostname = "localhost",
            Queue = "MyTest",
            RoutingKey = "MyTest",
            Massage = massage
        };
        return await _rabbitMqServise.Send(queryModel);
    }
}
