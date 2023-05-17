using Castle.Core.Logging;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Mp3Lyric.Extension;
using Mp3Lyric.Logging;
using Mp3Lyric.Mongodb.DbSetting;
using Mp3Lyric.Services;
using ILogger = Serilog.ILogger;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("Starting up");

var configurationBuilder = new ConfigurationBuilder()
     .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\")))
    //.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();


var services = new ServiceCollection();

var dbSettings = configurationBuilder.GetSection("DatabaseInfo");
services.Configure<DatabaseInfo>(dbSettings);
services.AddScoped<IDbContext, DbContext>();

services.AddSingleton(Log.Logger);
services.AddTransient<LoggingInterceptor>();
services.AddTransient<IInterceptor, LoggingInterceptor>();
services.AddLoggingInterceptor<ICrawlServices, CrawlServices>();
//services.AddScoped<ICrawlServices, CrawlServices>();

var serviceProvider = services.BuildServiceProvider();
var crawlService = (ICrawlServices)serviceProvider.GetService(typeof(ICrawlServices));

await crawlService.CrawlLyricAsync();