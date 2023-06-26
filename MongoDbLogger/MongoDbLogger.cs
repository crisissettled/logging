using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;



namespace MongoDbLogging {
    internal sealed class MongoDbLogger : ILogger {
        private readonly Func<MongoDbLoggerConfiguration> _getCurrentConfig;
        private readonly string _name;

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
            if (!IsEnabled(logLevel)) return;

            var config = _getCurrentConfig();
            var dbClient = new MongoClient(config.connectionUri);

            var db = dbClient.GetDatabase("logging");
            var collection = db.GetCollection<LoggingObject>("console_logging");
            var loggingObject = new LoggingObject(_name, $"Testing  {DateTime.Now.ToShortTimeString()}");
            collection.InsertOne(loggingObject);
        }
    }
}


internal record LoggingObject (string Name, string Message);
 