using AutoMapper;
using CQRS.Application.Blogs.Queries.GetBlogs;
using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandHandler(IBlogRepository repository, IMapper mapper) : IRequestHandler<CreateBlogCommand, BlogViewModel>
{
    private readonly IBlogRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<BlogViewModel> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blogEntity = new Blog { Name = request.Name, Description = request.Description, Author = request.Author };

        var result = await _repository.CreateAsync(blogEntity);

        return _mapper.Map<BlogViewModel>(result);
    }
}