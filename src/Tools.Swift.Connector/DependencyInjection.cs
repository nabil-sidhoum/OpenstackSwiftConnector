using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.Swift.Connector.Authentication;
using Tools.Swift.Connector.Configuration;

namespace Tools.Swift.Connector
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwiftClient(this IServiceCollection services, IConfiguration configuration)
        {
            SwiftAuthenticationConfig swiftConfig = configuration.GetSection("SwiftAuthentication").Get<SwiftAuthenticationConfig>();

            services.AddHttpClient("SwiftAuthenticationHttpClient")
                    .AddTypedClient<SwiftAuthentication>(httpclient => new(httpclient, swiftConfig));

            SwiftAuthentication authentication = services.BuildServiceProvider().GetService<SwiftAuthentication>();

            services.AddHttpClient("SwiftHttpClient")
                    .AddTypedClient<ISwiftClient>(httpclient => new OVHSwiftClient(httpclient, authentication));

            return services;
        }
    }
}
