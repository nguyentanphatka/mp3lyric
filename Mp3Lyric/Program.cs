using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Mp3Lyric.Mongodb.DbSetting;
using Mp3Lyric.Services;

var services = new ServiceCollection()
    .AddLogging(builder => builder.AddSerilog());

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("Starting up");

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\")))
    .AddJsonFile("config.json").Build();
var dbSettings = configurationBuilder.GetSection("DatabaseInfo");

services.AddScoped<ICrawlServices, CrawlServices>();
services.Configure<DatabaseInfo>(dbSettings);
services.AddScoped<IDbContext, DbContext>();

var serviceProvider = services.BuildServiceProvider();
var crawlService = (ICrawlServices)serviceProvider.GetService(typeof(ICrawlServices));

await crawlService.CrawlLyricAsync();