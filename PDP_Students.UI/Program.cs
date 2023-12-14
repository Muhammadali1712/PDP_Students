using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PDP_Students.Application;
using PDP_Students.Infrasturucture;
using PDP_Students.UI.CustomMiddlaware;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace PDP_Students.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddSignalR();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddLogging();
            builder.Services.AddApplicationServise();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(config =>
                {
                    config.SaveToken = true;
                    config.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddCors(p => p.AddPolicy("corspolicy",
                build =>
                {
                    build.WithOrigins("https://lms.tuit.uz").AllowAnyMethod().AllowAnyHeader();
                }));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExeptionMiddlaware();//.UseMiddleware<ExeptionMiddlaware>();
            app.UseCors("corspolicy");
            app.UseHttpsRedirection();
            app.UseRouting();

            /*app.UseCors(builder => builder
             .WithOrigins("http://localhost:5093")
             .AllowAnyMethod()
             .AllowAnyHeader()
             );*/

            app.UseAuthorization();
            app.UseRabbitMq();

            app.MapControllers();

            app.Run();
        }
    }
}
