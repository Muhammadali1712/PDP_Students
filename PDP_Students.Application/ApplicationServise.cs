using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PDP_Students.Application;

public static class ApplicationServise
{
    public static void AddApplicationServise(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
