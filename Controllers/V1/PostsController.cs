using AspNetCore_RestAPI.Models;
using AspNetCore_RestAPI.Routes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_RestAPI.Controllers.V1
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