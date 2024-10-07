using AutoMapper;
using CQRS.Application.Blogs.Commands.CreateBlog;
using CQRS.Application.Common.Models;
using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using Moq;

namespace CQRS.Test.Application.CommandHandlers;

public class CreateBlogCommandHandlerTests
{
    private readonly Mock<IBlogRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateBlogCommandHandler _handler;

    public CreateBlogCommandHandlerTests()
    {
        _mockRepository = new Mock<IBlogRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateBlogCommandHandler(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnBlogViewModel_WhenBlogIsCreated()
    {
        // Arrange
        var createBlogCommand = new CreateBlogCommand
        {
            Name = "Test Blog",
            Description = "Test Description",
            Author = "Test Author"
        };

        var blogEntity = new Blog
        {
            Name = createBlogCommand.Name,
            Description = createBlogCommand.Description,
            Author = createBlogCommand.Author
        };

        var blogViewModel = new BlogViewModel
        {
            Name = createBlogCommand.Name,
            Description = createBlogCommand.Description,
            Author = createBlogCommand.Author
        };

        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Blog>())).ReturnsAsync(blogEntity);
        _mockMapper.Setup(mapper => mapper.Map<BlogViewModel>(It.IsAny<Blog>())).Returns(blogViewModel);

        // Act
        var result = await _handler.Handle(createBlogCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(blogViewModel.Name, result.Name);
        Assert.Equal(blogViewModel.Description, result.Description);
        Assert.Equal(blogViewModel.Author, result.Author);
    }
}
