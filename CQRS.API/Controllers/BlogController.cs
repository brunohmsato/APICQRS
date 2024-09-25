using CQRS.Application.Blogs.Queries.GetBlogById;
using CQRS.Application.Blogs.Queries.GetBlogs;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var blogs = await Mediator.Send(new GetBlogQuery());
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var blog = await Mediator.Send(new GetBlogByIdQuery() { BlogId = id });

            if(blog is null)
                return NotFound();

            return Ok(blog);
        }
    }
}
