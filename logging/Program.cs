// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

ServiceCollection services = new ServiceCollection();

services.AddLogging(logBuilder => {
    logBuilder.AddConsole()
              .AddEventLog()
              .SetMinimumLevel(LogLevel.Information);
});

services.AddScoped<Test1>();


using (var sp = services.BuildServiceProvider()) {
    var test1 = sp.GetRequiredService<Test1>();
    test1.Test();
}



internal class Test1 {

    private readonly ILogger<Test1> _logger;

    public Test1(ILogger<Test1> logger) {
        _logger = logger;
    }


    public void Test() {
        try {
            _logger.LogDebug("Reading DB....");
            _logger.LogInformation("Open Database");
            _logger.LogWarning("Network error when reading DB");
            _logger.LogError("Database error");
            throw new Exception("unexcepted error to open DB");
        } catch (Exception ex) {

            _logger.LogError(ex, ex.ToString());
        }
    }

}