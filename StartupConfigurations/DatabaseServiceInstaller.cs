namespace AspNetCore_RestAPI.StartupConfigurations
{
    public class DatabaseServiceInstaller : IStartupConfigInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            var connectionString = configurations.GetConnectionString("DefaultConnection");
            
        }
    }
}