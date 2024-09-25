using MediatR;

namespace CQRS.Application.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommand : IRequest<int>
{
    public int Id { get; set; }
}