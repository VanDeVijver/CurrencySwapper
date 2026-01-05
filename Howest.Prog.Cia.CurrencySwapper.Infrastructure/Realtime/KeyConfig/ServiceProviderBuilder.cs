using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime.KeyConfig
{
    public static class ServiceProviderBuilder
    {
        public static IServiceProvider GetServiceProvider(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(ConfigurationOptions).Assembly)
                .AddCommandLine(args)
                .Build();
            var services = new ServiceCollection();

            services.Configure<ConfigurationOptions>(configuration.GetSection(typeof(ConfigurationOptions).FullName));

            var provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
