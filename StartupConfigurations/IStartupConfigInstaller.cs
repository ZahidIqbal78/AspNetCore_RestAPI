namespace dotnet_webapi_example.StartupConfigurations
{
    public interface IStartupConfigInstaller
    {
         void InstallService(IServiceCollection services, IConfiguration configurations);
    }
}