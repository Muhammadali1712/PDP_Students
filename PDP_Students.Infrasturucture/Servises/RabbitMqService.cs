using PDP_Students.Application.Servise;
using PDP_Students.Domain.Models;
using RabbitMQ.Client;
using System.Text;

namespace PDP_Students.Infrasturucture.Servises;

public class RabbitMqService : IRabbitMqServise
{
    public async Task<bool> Send(QueryModel query)
    {
        var factory = new ConnectionFactory { HostName = query.Hostname };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: query.Queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        string message = query.Massage;
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: query.RoutingKey,
                             basicProperties: null,
                             body: body);
        return true;
    }
}
