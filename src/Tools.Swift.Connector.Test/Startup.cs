using Tools.Swift.Connector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.Swift.Test
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);

            services.AddSwiftClient(configuration);

            return services;
        }
    }
}
