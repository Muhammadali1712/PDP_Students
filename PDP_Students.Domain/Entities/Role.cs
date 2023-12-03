using System.Text.Json.Serialization;

namespace PDP_Students.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Student> Students { get; set; }
}
