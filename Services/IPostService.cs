using AspNetCore_RestAPI.Models;

namespace dotnet_webapi_example.Services
{
    public interface IPostService
    {
        List<Post> GetPosts();
        Post GetPostById(Guid postId);
        bool UpdatePost(Post post);
        bool DeletePost(Guid postId);
    }
}
