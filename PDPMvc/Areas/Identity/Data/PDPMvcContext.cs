using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PDPMvc.Areas.Identity.Data;

namespace PDPMvc.Data;

public class PDPMvcContext : IdentityDbContext<PDPMvcUser>
{
    public PDPMvcContext(DbContextOptions<PDPMvcContext> options)
        : base(options)
    {
    }


    public DbSet<PDPMvcUser> PDPMvcUsers { get; set; }
}
