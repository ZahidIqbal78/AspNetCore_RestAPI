using AspNetCore_RestAPI.Models;

namespace dotnet_webapi_example.Services
{
    public class PostService : IPostService
    {
        private readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();

            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post
                {
                    Id = Guid.NewGuid(),
                    Name = $"New Post {i}"
                });
            }
        }

        public bool DeletePost(Guid postId)
        {
            var post = GetPostById(postId);
            if (post is null) return false;
            
            _posts.Remove(post);
            return true;
        }

        public Post GetPostById(Guid postId)
        {
            return _posts.SingleOrDefault(x => x.Id == postId);
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public bool UpdatePost(Post post)
        {
            var postExists = GetPostById(post.Id) != null;

            if (!postExists) return false;

            var index = _posts.FindIndex(x=> x.Id == post.Id);
            _posts[index] = post;
            return true;
        }
    }
}
