using PDP_Students.Domain.States;

namespace PDP_Students.Domain.Models.StudentDTO;

public class StudentBaseDTO
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public string StudyPlace { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }

    public int MentorId { get; set; }
    public IEnumerable<int>? RoleId { get; set; }
}
