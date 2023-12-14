using Microsoft.AspNetCore.Mvc.Filters;

namespace PDP_Students.UI.CustomFilters;

public class CustomResultFilterAttribute : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        await Console.Out.WriteLineAsync("OnResultExecutionAsync Started");
        await next();
        await Console.Out.WriteLineAsync("OnResultExecutionAsync Ended");
    }
}
