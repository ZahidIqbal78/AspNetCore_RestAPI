using dotnet_webapi_example.Models;
using dotnet_webapi_example.Routes;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_example.Controllers.V1
{
    public class PostsController : ControllerBase
    {
        private List<Post> _tasks;

        public PostsController()
        {
            _tasks = new List<Post>();

            for (var i = 0; i < 5; i++)
            {
                _tasks.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_tasks);
        }
    }
}