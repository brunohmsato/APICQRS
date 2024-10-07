using AutoMapper;
using CQRS.Application.Blogs.Queries.GetBlogById;
using CQRS.Application.Common.Models;
using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using Moq;

namespace CQRS.Test.Application.QueryHandlers;

public class GetBlogByIdQueryHandlerTests
{
    private readonly Mock<IBlogRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBlogByIdQueryHandler _handler;

    public GetBlogByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IBlogRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetBlogByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnBlogViewModel_WhenBlogExists()
    {
        // Arrange
        var blogId = 1;
        var blog = new Blog
        {
            Id = blogId,
            Name = "Test Blog",
            Description = "Test Description",
            Author = "Test Author"
        };

        var blogViewModel = new BlogViewModel
        {
            Id = blogId,
            Name = blog.Name,
            Description = blog.Description,
            Author = blog.Author
        };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(blogId)).ReturnsAsync(blog);
        _mapperMock.Setup(mapper => mapper.Map<BlogViewModel>(blog)).Returns(blogViewModel);

        var request = new GetBlogByIdQuery { BlogId = blogId };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(blogId, result.Id);
        Assert.Equal("Test Blog", result.Name);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal("Test Author", result.Author);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenBlogDoesNotExist()
    {
        // Arrange
        var blogId = 1;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(blogId)).ReturnsAsync((Blog)null);

        var request = new GetBlogByIdQuery { BlogId = blogId };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}