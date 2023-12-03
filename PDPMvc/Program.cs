using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PDP_Students.Application;
using PDP_Students.Infrasturucture;
using PDP_Students.Infrasturucture.DataAcces;
using System.Text;

namespace PDPMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructureServices(builder.Configuration);

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

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
