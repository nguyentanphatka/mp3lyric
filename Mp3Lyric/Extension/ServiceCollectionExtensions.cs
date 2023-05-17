using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Mp3Lyric.Logging;

namespace Mp3Lyric.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggingInterceptor<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        //services.AddTransient<TImplementation>();
        //services.AddTransient<TService>(provider =>
        services.AddScoped<TImplementation>();
        services.AddScoped<TService>(provider =>
        {
            var serilogLogger = provider.GetService<Serilog.ILogger>();
            var castleLogger = provider.GetService<Castle.Core.Logging.ILogger>();
            var interceptor = new LoggingInterceptor(serilogLogger, castleLogger);
            var implementation = provider.GetService<TImplementation>();

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TService>(implementation, interceptor);

            return proxy;
        });

        return services;
    }
}