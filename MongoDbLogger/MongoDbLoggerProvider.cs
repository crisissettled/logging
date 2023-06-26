using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;


namespace MongoDbLogging {
    internal sealed class MongoDbLoggerProvider : ILoggerProvider {
        private IOptionsSnapshot<MongoDbLoggerConfiguration> _config;
        private readonly ConcurrentDictionary<string, MongoDbLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public MongoDbLoggerProvider(IOptionsSnapshot<MongoDbLoggerConfiguration> config) {
            this._config = config;
         }

        public ILogger CreateLogger(string categoryName) {
            return _loggers.GetOrAdd(categoryName, (categoryName) => new MongoDbLogger(categoryName, GetCurrentConfig));
        }

        private MongoDbLoggerConfiguration GetCurrentConfig() => _config.Value;

        public void Dispose() {
            _loggers.Clear(); 
        }        
    }
}
