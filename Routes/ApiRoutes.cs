namespace AspNetCore_RestAPI.Routes
{
    public static class ApiRoutes
    {
        public const string RootAddress = "api";
        public const string Version = "v1";
        public const string BaseAddress = RootAddress + "/" + Version;

        public static class Posts
        {
            public const string GetAll = BaseAddress + "/posts";
            public const string Create = BaseAddress + "/posts";
            public const string Get = BaseAddress + "/posts/{postId}";
            public const string Update = BaseAddress + "/posts/{postId}";
            public const string Delete = BaseAddress + "/posts/{postId}";
        }

        public static class Identity
        {
            public const string Login = BaseAddress + "/identity/login";
            public const string Register = BaseAddress + "/identity/register";
        }
    }
}