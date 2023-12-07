namespace PDP_Students.Domain.Entities;

public class Student : BaseEntity
{
    public string? StudyPlace { get; set; }
    public int? MentorId { get; set; }
    public Mentor? Mentor { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }
}
