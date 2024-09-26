using CQRS.Application.Common.Models;
using MediatR;

namespace CQRS.Application.Blogs.Queries.GetBlogs;

public class GetBlogQuery : IRequest<List<BlogViewModel>>
{
}