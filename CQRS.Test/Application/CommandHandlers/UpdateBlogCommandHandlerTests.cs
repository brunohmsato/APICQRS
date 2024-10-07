using CQRS.Application.Blogs.Commands.UpdateBlog;
using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using Moq;

namespace CQRS.Test.Application.CommandHandlers;

public class UpdateBlogCommandHandlerTests
{
    private readonly Mock<IBlogRepository> _mockRepository;
    private readonly UpdateBlogCommandHandler _handler;

    public UpdateBlogCommandHandlerTests()
    {
        _mockRepository = new Mock<IBlogRepository>();
        _handler = new UpdateBlogCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUpdatedEntityId_WhenUpdateIsSuccessful()
    {
        // Arrange
        var command = new UpdateBlogCommand
        {
            Id = 1,
            Name = "Updated Blog Name",
            Description = "Updated Blog Description",
            Author = "Updated Author"
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<Blog>()))
            .ReturnsAsync(command.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(command.Id, result);
        _mockRepository.Verify(repo => repo.UpdateAsync(command.Id, It.IsAny<Blog>()), Times.Once);
    }
}
