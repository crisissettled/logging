// See https://aka.ms/new-console-template for more information
using logging.CustomLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

Console.WriteLine(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") + ", " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + " - DOTNET_ENVIRONMENT, ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

var hostBuilder = Host.CreateDefaultBuilder(args)
   //.ConfigureAppConfiguration(configBuilder => configBuilder.AddJsonFile("appsettings.json", false, true))
   .ConfigureLogging(logBuilder =>
        logBuilder.ClearProviders()
           .AddConfiguration(configuration.GetSection("LoggingDatabase"))
           .AddColorConsoleLogger(configuration => {
               // Replace warning value from appsettings.json of "Cyan"
               //configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
               // Replace warning value from appsettings.json of "Red"
               //configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
           }))
   .ConfigureServices(services => {
       services.AddOptions().Configure<LoggingDatabase>(configuration.GetSection("LoggingDatabase"));
       services.AddScoped<Test1>();
   });
//.ConfigureServices((hostBuilderContext,services) => {
//    services.AddOptions().Configure<LoggingDatabase>(hostBuilderContext.Configuration.GetSection("LoggingDatabase"));
//    services.AddScoped<Test1>();
//});


using IHost host = hostBuilder.Build();


var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogDebug(1, "Does this line get hit?");    // Not logged
logger.LogInformation(3, "Nothing to see here."); // Logs in ConsoleColor.DarkGreen
logger.LogWarning(5, "Warning... that was odd."); // Logs in ConsoleColor.DarkCyan
logger.LogError(7, "Oops, there was an error.");  // Logs in ConsoleColor.DarkRed
logger.LogTrace(5, "== 120.");                    // Not logged


var test1 = host.Services.GetRequiredService<Test1>();
test1.Test();

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






class LoggingDatabase {
    public string? DbName { get; set; }
    public int Port { get; set; }
    public string? UserName { get; set; }
}

class Secret {
    private readonly string secret = "abc980WF";

    public string GetSecret() {
        return secret;
    }
}

internal class Test1 {

    private readonly ILogger<Test1> _logger;
    private readonly IOptionsMonitor<LoggingDatabase> loggingDatabaseOptions;
    public Test1(ILogger<Test1> logger, IOptionsMonitor<LoggingDatabase> options) {
        this.loggingDatabaseOptions = options;
        _logger = logger;


        options.OnChange(loggingDb => {
            Console.WriteLine(loggingDb.DbName);
        });

        Console.WriteLine(this.loggingDatabaseOptions.CurrentValue.DbName + " in Test1 constructor");


    }

    public IOptionsMonitor<LoggingDatabase> Options { get; }

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