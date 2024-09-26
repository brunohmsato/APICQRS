using CQRS.Application.Common.Models;
using MediatR;

namespace CQRS.Application.Blogs.Queries.GetBlogById;

public class GetBlogByIdQuery : IRequest<BlogViewModel>
{
    public int BlogId { get; set; }
}