using PDP_Students.Domain.Entities;
using PDP_Students.Domain.States;

namespace PDP_Students.Domain.Models.MentorDTO;

public class MentorBaseDTO
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
   
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string Workplace { get; set; }
    public string Level { get; set; }

    public IEnumerable<int> StudentsId { get; set; }
}
