using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace MongoDbLogging {
    public static class MongoDbLoggerExtension {
        public static ILoggingBuilder AddMongoDbLogger(this ILoggingBuilder builder, IConfiguration config) {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, MongoDbLoggerProvider>());
            builder.Services.Configure<MongoDbLoggerConfiguration>(config);

            return builder;
        }
    }
}
