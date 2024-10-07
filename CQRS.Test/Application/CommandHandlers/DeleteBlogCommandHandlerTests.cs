using CQRS.Application.Blogs.Commands.DeleteBlog;
using CQRS.Domain.Repository;
using Moq;

namespace CQRS.Test.Application.CommandHandlers;

public class DeleteBlogCommandHandlerTests
{
    private readonly Mock<IBlogRepository> _mockRepository;
    private readonly DeleteBlogCommandHandler _handler;

    public DeleteBlogCommandHandlerTests()
    {
        _mockRepository = new Mock<IBlogRepository>();
        _handler = new DeleteBlogCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDeletedBlogId()
    {
        // Arrange
        var blogId = 1;
        var command = new DeleteBlogCommand { Id = blogId };
        _mockRepository.Setup(repo => repo.DeleteAsync(blogId)).ReturnsAsync(blogId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(blogId, result);
        _mockRepository.Verify(repo => repo.DeleteAsync(blogId), Times.Once);
    }
}
