using CQRS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
}
