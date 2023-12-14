namespace PDP_Students.Domain.Models;

public class QueryModel
{
    public string Hostname {  get; set; }
    public string Queue { get; set; }
    public string Massage { get; set; }
    public string RoutingKey { get; set; }
}
