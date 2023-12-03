namespace PDP_Students.Domain.Models.StudentDTO;

public class StudentUpdateDTO:StudentBaseDTO
{
    public int Id { get; set; }
    public string Password { get; set; }
}
