using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FS.WebAPI.DI
{
    public static class Serilog
    {
        public static IServiceCollection AddSerilogServices(
        this IServiceCollection services,
        LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}
