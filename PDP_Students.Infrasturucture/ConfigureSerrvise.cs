using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PDP_Students.Application.Servise;
using PDP_Students.Infrasturucture.DataAcces;
using PDP_Students.Infrasturucture.Servises;

namespace PDP_Students.Infrasturucture;

public static class ConfigureSerrvise
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenServise, TokenServise>();
        services.AddScoped<IStudentServise, StudentServise>();
        services.AddScoped<IMentorServise, MentorServise>();
        services.AddScoped<IRoleServise, RoleServise>();
        services.AddScoped<IStudentCRUDServise, StudentCRUDServise>();
        services.AddDbContext<PDP_StudentDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("PDPConnection")));
    }
}
