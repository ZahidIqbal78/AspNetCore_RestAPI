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
        }

        public static class Identity
        {
            public const string Login = BaseAddress + "/identity/login";
            public const string Register = BaseAddress + "/identity/register";
        }
    }
}