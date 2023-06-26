using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace logging.CustomLogging {

    [UnsupportedOSPlatform("browser")]
    //[ProviderAlias("ColorConsole")]
    public sealed class ColorConsoleLoggerProvider : ILoggerProvider {
        private readonly IDisposable? _onConfigChange;
        private ColorConsoleLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public ColorConsoleLoggerProvider(IOptionsMonitor<ColorConsoleLoggerConfiguration> config) {
            _config = config.CurrentValue;
            _onConfigChange = config.OnChange(updatedConfig => _config = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => 
            _loggers.GetOrAdd(categoryName,
                categoryName => {
                    return new ColorConsoleLogger(categoryName, GetCurrentConfig);
            });


        private ColorConsoleLoggerConfiguration GetCurrentConfig() => _config;

        public void Dispose() {
            _loggers.Clear();
            _onConfigChange?.Dispose();
        }
    }
}
