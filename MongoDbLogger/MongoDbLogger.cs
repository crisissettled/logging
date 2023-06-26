using Microsoft.Extensions.Logging;

namespace MongoDbLogging {
    internal sealed class MongoDbLogger : ILogger {
        private readonly object _getCurrentConfig;
        private readonly object _name;

        public MongoDbLogger(string name, Func<MongoDbLoggerConfiguration> getCurrentConfig) {
            (_name, _getCurrentConfig) = (name, getCurrentConfig);
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            throw new NotImplementedException();
        }
    }
}
