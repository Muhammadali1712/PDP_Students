using PDP_Students.Domain.Entities;

namespace PDP_Students.Domain.Models;

public class GetRegisterModel
{
    public Token token { get; set; }
    public Student student { get; set; }
}
