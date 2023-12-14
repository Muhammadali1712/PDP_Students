using Microsoft.AspNetCore.Mvc.Filters;

namespace PDP_Students.UI.CustomFilters;

public class CustomAutorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("OnAuthorization");
    }
}
