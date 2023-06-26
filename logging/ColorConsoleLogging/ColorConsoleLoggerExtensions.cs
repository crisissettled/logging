using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;

namespace logging.CustomLogging {
    public static class ColorConsoleLoggerExtensions {
        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder) {
            //builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());

            //LoggerProviderOptions.RegisterProviderOptions<ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, IConfiguration config) {
            //builder.AddColorConsoleLogger();
            //builder.Services.AddSingleton<ILoggerProvider, ColorConsoleLoggerProvider>();     
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>()); 
            builder.Services.Configure<ColorConsoleLoggerConfiguration>(config);

            return builder;
        }
    }
}
