using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;



namespace MongoDbLogging {
    internal sealed class MongoDbLogger : ILogger {
        private readonly Func<MongoDbLoggerConfiguration> _getCurrentConfig;
        private readonly string _environment;
        private readonly string _serviceName;
        private readonly string _name;

        public MongoDbLogger(string environment, string serviceName,string name, Func<MongoDbLoggerConfiguration> getCurrentConfig) {
            (this._environment, this._serviceName, _name, _getCurrentConfig) = (environment, serviceName, name, getCurrentConfig);
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
            if (!IsEnabled(logLevel)) return;
            
            var loggingEntity = new MongoDbLoggingEntity(_environment,_serviceName,logLevel.ToString(), _name, $"{formatter(state, exception)}");
            InserLoggingToMongoDb(loggingEntity);
        }

        private void InserLoggingToMongoDb(MongoDbLoggingEntity loggingEntity) {
            var config = _getCurrentConfig();
            var dbClient = new MongoClient(config.connectionUri);

            var db = dbClient.GetDatabase("logging");
            var collection = db.GetCollection<MongoDbLoggingEntity>("fsp_logging");

            collection.InsertOne(loggingEntity);
        }
    }
}



 