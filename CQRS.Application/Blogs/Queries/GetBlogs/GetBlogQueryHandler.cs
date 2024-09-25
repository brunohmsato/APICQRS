using AutoMapper;
using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Blogs.Queries.GetBlogs;

public class GetBlogQueryHandler(IBlogRepository repository, IMapper mapper) : IRequestHandler<GetBlogQuery, List<BlogViewModel>>
{
    private readonly IBlogRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<BlogViewModel>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
    {
        var blogs = await _repository.GetAllBlogsAsync();

        /*
        var blogList = blogs.Select(x => new BlogViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Author = x.Author,
        }).ToList();
        */

        var blogList = _mapper.Map<List<BlogViewModel>>(blogs);

        return blogList;
    }
}