using MediatR;

namespace CQRS.Application.Blogs.Queries.GetBlogs;

public class GetBlogQuery : IRequest<List<BlogViewModel>>
{
}