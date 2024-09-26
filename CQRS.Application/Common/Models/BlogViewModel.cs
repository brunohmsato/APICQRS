using CQRS.Application.Common.Mappings;
using CQRS.Domain.Entity;

namespace CQRS.Application.Common.Models;

public class BlogViewModel : IMapFrom<Blog>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
}