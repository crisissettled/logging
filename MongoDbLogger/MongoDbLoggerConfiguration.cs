namespace MongoDbLogging {
    internal sealed class MongoDbLoggerConfiguration {
        public string Host { get; set; }
        public int Port { get; set; }

        public MongoDbLoggerConfiguration(string host, int port) {
            this.Host = host;
            this.Port = port;
        }
    }
}
