using CQRS.Application.Blogs.Commands.CreateBlog;
using CQRS.Application.Blogs.Commands.UpdateBlog;
using NBomber.CSharp;
using Newtonsoft.Json;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var httpClient = new HttpClient();

        /*async Task<int> CreateBlogAsync()
        {
            var newBlog = new CreateBlogCommand
            {
                Name = "Temporary Blog",
                Description = "This blog will be deleted.",
                Author = "Test Author"
            };

            var content = new StringContent(JsonConvert.SerializeObject(newBlog), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7202/api/Blog", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(responseData);
            }

            return -1; // Indicando erro  
        }*/

        #region Cenario para GetAll
        var getAllScenario = Scenario.Create("get_all_blogs_scenario", async context =>
        {
            var response = await httpClient.GetAsync("https://localhost:7202/api/Blog");
            return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(
            Simulation.RampingInject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
            Simulation.Inject(rate: 20, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
        );
        #endregion Cenario para GetAll

        #region Cenario para GetById
        var getByIdScenario = Scenario.Create("get_blog_by_id_scenario", async context =>
        {
            var existentBlogIds = new List<int> { 3, 4, 5, 6 };

            var random = new Random();
            int randomId = existentBlogIds[random.Next(existentBlogIds.Count)];

            var response = await httpClient.GetAsync($"https://localhost:7202/api/Blog/{randomId}");
            return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));

        var getNotFoundScenario = Scenario.Create("get_blog_by_nonexistent_id_scenario", async context =>
        {
            var nonexistentBlogIds = new List<int> { 1, 2 };

            var random = new Random();
            int randomId = nonexistentBlogIds[random.Next(nonexistentBlogIds.Count)];

            var response = await httpClient.GetAsync($"https://localhost:7202/api/Blog/{randomId}");
            return response.StatusCode == HttpStatusCode.NotFound ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));
        #endregion Cenario para GetById

        #region Cenario para Create
        var createScenario = Scenario.Create("create_blog_scenario", async context =>
        {
            var createBlogCommand = new CreateBlogCommand
            {
                Name = "New Blog",
                Description = "Blog content",
                Author = "Bruno Sato"
            };

            var content = new StringContent(JsonConvert.SerializeObject(createBlogCommand), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7202/api/Blog", content);

            return response.StatusCode == HttpStatusCode.Created ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));

        var createInvalidScenario = Scenario.Create("create_invalid_blog_scenario", async context =>
        {
            var invalidBlog = new CreateBlogCommand
            {
                Name = "",
                Description = null,
                Author = null
            };

            var content = new StringContent(JsonConvert.SerializeObject(invalidBlog), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7202/api/Blog", content);

            return response.StatusCode == HttpStatusCode.BadRequest ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));
        #endregion Cenario para Create

        #region Cenario para Update
        var updateScenario = Scenario.Create("update_blog_scenario", async context =>
        {
            var blogIdToUpdate = 3; // id a ser atualizado 

            var updateBlogCommand = new UpdateBlogCommand
            {
                Id = blogIdToUpdate,
                Name = "Updated Blog Name",
                Description = "Updated Blog Description",
                Author = "Updated Author"
            };

            var content = new StringContent(JsonConvert.SerializeObject(updateBlogCommand), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7202/api/Blog/{blogIdToUpdate}", content);

            return response.StatusCode == HttpStatusCode.NoContent ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));

        var updateMismatchScenario = Scenario.Create("update_mismatch_scenario", async context =>
        {
            var invalidId = 99; // id que não existe  
            var updateBlogCommand = new UpdateBlogCommand
            {
                Id = 1, // id válido mas que não corresponde  
                Name = "Updated Blog Name",
                Description = "Updated Blog Description",
                Author = "Updated Author"
            };

            var content = new StringContent(JsonConvert.SerializeObject(updateBlogCommand), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7202/api/Blog/{invalidId}", content);

            return response.StatusCode == HttpStatusCode.BadRequest ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));
        #endregion Cenario para Update

        #region Cenario para Delete
        /*var deleteScenario = Scenario.Create("delete_blog_scenario", async context =>
        {
            var blogIds = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                var blogId = await CreateBlogAsync();
                if (blogId != -1)
                {
                    blogIds.Add(blogId);
                }
            }

            if (blogIds.Count == 0)
                return Response.Fail();

            var id = blogIds.FirstOrDefault(); 
            blogIds.RemoveAt(0); 

            var response = await httpClient.DeleteAsync($"https://localhost:7202/api/Blog/{id}");

            return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));*/

        var deleteNotFoundScenario = Scenario.Create("delete_not_found_scenario", async context =>
        {
            var invalidBlogId = 99;

            var response = await httpClient.DeleteAsync($"https://localhost:7202/api/Blog/{invalidBlogId}");
            return response.StatusCode == HttpStatusCode.NotFound ? Response.Ok() : Response.Fail();
        })
        .WithLoadSimulations(Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)));
        #endregion Cenario para Delete

        NBomberRunner
            .RegisterScenarios(
            getAllScenario
            , getByIdScenario
            , getNotFoundScenario
            , createScenario
            , createInvalidScenario
            , updateScenario
            , updateMismatchScenario
            //, deleteScenario
            , deleteNotFoundScenario
            )
            .Run();
    }
}