namespace AspNetCore_RestAPI.StartupConfigurations
{
    public interface IStartupConfigInstaller
    {
         void InstallService(IServiceCollection services, IConfiguration configurations);
    }
}