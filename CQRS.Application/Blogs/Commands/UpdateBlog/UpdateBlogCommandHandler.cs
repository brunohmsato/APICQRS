using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommandHandler(IBlogRepository repository) : IRequestHandler<UpdateBlogCommand, int>
{
    private readonly IBlogRepository _repository = repository;

    public async Task<int> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var updatedEntity = new Blog()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Author = request.Author
        };

        return await _repository.UpdateAsync(request.Id, updatedEntity);
    }
}