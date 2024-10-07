using CQRS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infra.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Blog> Blogs { get; set; }
}
