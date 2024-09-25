using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommandHandler(IBlogRepository repository) : IRequestHandler<DeleteBlogCommand, int>
{
    private readonly IBlogRepository _repository = repository;

    public async Task<int> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}