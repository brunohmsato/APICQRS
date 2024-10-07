using AutoMapper;
using CQRS.Application.Blogs.Queries.GetBlogs;
using CQRS.Application.Common.Models;
using CQRS.Domain.Entity;
using CQRS.Domain.Repository;
using Moq;

namespace CQRS.Test.Application.QueryHandlers;

public class GetBlogQueryHandlerTests
{
    private readonly Mock<IBlogRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBlogQueryHandler _handler;

    public GetBlogQueryHandlerTests()
    {
        _repositoryMock = new Mock<IBlogRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetBlogQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedBlogList()
    {
        // Arrange
        var blogs = new List<Blog> {
            new Blog 
            {
                Id = 1,
                Name = "Test Blog",
                Description = "Test Description",
                Author = "Test Author"
            }
        };
        var blogViewModels = new List<BlogViewModel> {
            new BlogViewModel
            {
                Id = 1,
                Name = "Test Blog",
                Description = "Test Description",
                Author = "Test Author"
            }
        };

        _repositoryMock.Setup(repo => repo.GetAllBlogsAsync()).ReturnsAsync(blogs);
        _mapperMock.Setup(mapper => mapper.Map<List<BlogViewModel>>(blogs)).Returns(blogViewModels);

        var query = new GetBlogQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(blogViewModels, result);
    }
}
