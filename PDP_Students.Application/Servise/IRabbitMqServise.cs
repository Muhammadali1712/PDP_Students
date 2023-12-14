using PDP_Students.Domain.Models;

namespace PDP_Students.Application.Servise;

public interface IRabbitMqServise
{
    Task<bool> Send(QueryModel query);
}
