using AspNetCore_RestAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore_RestAPI.StartupConfigurations
{
    public class DatabaseServiceInstaller : IStartupConfigInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            var connectionString = configurations.GetConnectionString("DefaultConnection");
            services.AddDbContext<TestDbContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
        }
    }
}