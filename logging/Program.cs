// See https://aka.ms/new-console-template for more information


using logging.CustomLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args)
   .ConfigureLogging(logBuilder =>
        logBuilder.ClearProviders()
           .AddColorConsoleLogger(configuration => {
               // Replace warning value from appsettings.json of "Cyan"
               configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
               // Replace warning value from appsettings.json of "Red"
               configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
    }))
    .Build();


var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogDebug(1, "Does this line get hit?");    // Not logged
logger.LogInformation(3, "Nothing to see here."); // Logs in ConsoleColor.DarkGreen
logger.LogWarning(5, "Warning... that was odd."); // Logs in ConsoleColor.DarkCyan
logger.LogError(7, "Oops, there was an error.");  // Logs in ConsoleColor.DarkRed
logger.LogTrace(5, "== 120.");                    // Not logged

await host.RunAsync();

//Console.WriteLine(new Secret().GetSecret());

//IServiceCollection services = new ServiceCollection();

//services.AddLogging(logBuilder => logBuilder.AddColorConsoleLogger());

////services.AddLogging(logBuilder => {
////    logBuilder.ClearProviders();
////    logBuilder.AddConsole()
////              .AddEventLog()
////              .SetMinimumLevel(LogLevel.Information);

////    Console.WriteLine(logBuilder.Services.Equals(services));


////});

//services.AddScoped<Test1>();


//using (var sp = services.BuildServiceProvider()) {
//    var test1 = sp.GetRequiredService<Test1>();
//    test1.Test();
//}








class Secret {
    private readonly string secret = "abc980WF";

    public string GetSecret() {
        return secret;
    }
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