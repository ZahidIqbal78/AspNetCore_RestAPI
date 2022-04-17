namespace AspNetCore_RestAPI.StartupConfigurations
{
    public static class InstallStartupServices
    {
        public static void InstallServicetoAssembly(this IServiceCollection services, IConfiguration configurations)
        {
            var startupConfigInstallers = typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IStartupConfigInstaller).IsAssignableFrom(x)
                    && !x.IsInterface
                    && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IStartupConfigInstaller>().ToList();

            startupConfigInstallers.ForEach(
                configs => configs.InstallService(services, configurations)
            );

        }
    }
}