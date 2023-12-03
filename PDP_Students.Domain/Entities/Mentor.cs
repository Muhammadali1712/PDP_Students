namespace PDP_Students.Domain.Entities;

public class Mentor:BaseEntity
{
    public string Workplace { get; set; }
    public string Level { get; set; }

    public ICollection<Student> Students { get; set; }

}
