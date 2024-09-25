using AutoMapper;
using CQRS.Application.Blogs.Queries.GetBlogs;
using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Blogs.Queries.GetBlogById;

public class GetBlogByIdQueryHandler(IBlogRepository repository, IMapper mapper) : IRequestHandler<GetBlogByIdQuery, BlogViewModel>
{
    private readonly IBlogRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<BlogViewModel> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetByIdAsync(request.BlogId);

        return _mapper.Map<BlogViewModel>(blog); ;
    }
}