namespace PDP_Students.Domain.Models.StudentDTO;

public class StudentCreateDTO:StudentBaseDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
