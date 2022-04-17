namespace AspNetCore_RestAPI.StartupConfigurations
{
    public class WebApiServiceInstaller : IStartupConfigInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(x => 
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
                    Title = "Dotnet WebAPI",
                    Version ="v1",
                });
            });
        }
    }
}