using AspNetCore_RestAPI.Models;
using AspNetCore_RestAPI.Routes;
using dotnet_webapi_example.DTOs.V1.Requests;
using dotnet_webapi_example.DTOs.V1.Responses;
using dotnet_webapi_example.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_RestAPI.Controllers.V1
{
    /// <summary>
    /// This is a test controller to demonstrate the flow or the structure, no real
    /// database operations for Post model.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetPostById(postId);
            if(post is null) return NotFound();
            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };
            if(post.Id == Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }

            _postService.GetPosts().Add(post);
            var baseAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseAddress + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUrl, response);
        }
    
        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var isUpdated = _postService.UpdatePost(post);
            if (isUpdated) return Ok(post);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var isDeleted = _postService.DeletePost(postId);
            if (!isDeleted) return NotFound();
            return NoContent();
        }
    }
}