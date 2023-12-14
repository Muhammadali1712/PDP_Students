
using Newtonsoft.Json;
using System.Net;

namespace PDP_Students.UI.CustomMiddlaware;

public class ExeptionMiddlaware
{
    private readonly ILogger<ExeptionMiddlaware> _logger;
    private readonly RequestDelegate _next;

    public ExeptionMiddlaware(RequestDelegate next, ILogger<ExeptionMiddlaware> logger)
    {

        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Return a custom JSON response
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { error = "Serverda xatolik bor! Birozdan so'ng urunib ko'ring!!!" });
            await context.Response.WriteAsync(result);
        }
    }
}
public static class ExeptionMiddlawareExtensions
{
    public static IApplicationBuilder UseExeptionMiddlaware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionMiddlaware>();
    }
}
