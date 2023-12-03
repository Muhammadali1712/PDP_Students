using Microsoft.EntityFrameworkCore;
using PDP_Students.Domain.Entities;

namespace PDP_Students.Infrasturucture.DataAcces;

public class PDP_StudentDbContext:DbContext
{
    public PDP_StudentDbContext()
    {
    }

    public PDP_StudentDbContext(DbContextOptions<PDP_StudentDbContext> options ):base(options)
    {
    }

    public DbSet<Role> Roles {  get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

}
