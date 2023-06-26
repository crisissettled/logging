namespace MongoDbLogging {
    internal sealed class MongoDbLoggerConfiguration {
        public string? Host { get; set; }
        public string? Port { get; set; }

        public string connectionUri {
            get {
                return $"mongodb://{Host}:{Port}";
            }
        }      
    }
}
