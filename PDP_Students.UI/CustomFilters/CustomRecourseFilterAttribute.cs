using Microsoft.AspNetCore.Mvc.Filters;

namespace PDP_Students.UI.CustomFilters;

public class CustomRecourseFilterAttribute : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        Console.WriteLine("OnResourceExecutionAsync Started");
        await next();
        Console.WriteLine("OnResourceExecutionAsync Ended");
    }
}
