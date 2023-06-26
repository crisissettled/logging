using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace logging.CustomLogging {
    public static class ColorConsoleLoggerExtensions {
        //public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder) {
        //    //builder.AddConfiguration();

        //    builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());

        //    //LoggerProviderOptions.RegisterProviderOptions<ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

        //    return builder;
        //}

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, IConfiguration config) {
            //builder.AddColorConsoleLogger();
            //builder.Services.AddSingleton<ILoggerProvider, ColorConsoleLoggerProvider>();     
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>()); 
            builder.Services.Configure<ColorConsoleLoggerConfiguration>(config);

            return builder;
        }
    }
}
