using Microsoft.Extensions.Configuration;
using System;

namespace Laren.E2ETests
{
    public class ConfigurationFactory
    {
        public IConfiguration CreateInstance()
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Staging";
            var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            return configuration;
        }
    }
}
