using PDP_Students.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PDP_Students.UI.CustomMiddlaware;

public class RabbitMqGetMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public RabbitMqGetMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public Task Invoke(HttpContext httpContext)
    {
        string hostName =_configuration["RabbitMq:HostName"];
        string queue =_configuration["RabbitMq:Queue"];
        var factory = new ConnectionFactory { HostName = hostName };
        using var connection = factory.CreateConnection(); 
        using var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };
        channel.BasicConsume(queue: queue,
                             autoAck: true,
                             consumer: consumer);

        return _next(httpContext);
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRabbitMq(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RabbitMqGetMiddleware>();
    }
}
