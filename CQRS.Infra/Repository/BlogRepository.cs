using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using CQRS.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infra.Repository;

public class BlogRepository(ApplicationDbContext context) : IBlogRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Blog> CreateAsync(Blog blog)
    {
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _context.Blogs
                        .Where(model => model.Id == id)
                        .ExecuteDeleteAsync();
    }

    public async Task<List<Blog>> GetAllBlogsAsync()
    {
        return await _context.Blogs
                        .ToListAsync();
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        var blog = await _context.Blogs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
            return null;

        return blog;
    }

    public async Task<int> UpdateAsync(int id, Blog blog)
    {
        return await _context.Blogs
                        .Where(model => model.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(m => m.Id, blog.Id)
                            .SetProperty(m => m.Name, blog.Name)
                            .SetProperty(m => m.Description, blog.Description)
                            .SetProperty(m => m.Author, blog.Author)
                        );
    }
}
