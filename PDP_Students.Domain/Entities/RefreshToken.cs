namespace PDP_Students.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string RefreshTokenValue { get; set; }
    public DateTime ExpiredTime { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

}
