using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PDP_Students.Application;
using PDP_Students.Infrasturucture;
using PDPMvc.Areas.Identity.Data;
using PDPMvc.Data;
using System.Text;

namespace PDPMvc
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("PDPConnection") ?? throw new InvalidOperationException("Connection string 'PDPMvcContextConnection' not found.");

			builder.Services.AddDbContext<PDPMvcContext>(options => options.UseNpgsql(connectionString));

			builder.Services.AddDefaultIdentity<PDPMvcUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PDPMvcContext>();

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			builder.Services.AddControllersWithViews();

			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddRazorPages();
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
			app.MapRazorPages();
			app.Run();
		}
	}
}
