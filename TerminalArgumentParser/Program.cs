using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TerminalArgumentParser.Interfaces;
using TerminalArgumentParser.Models;
using TerminalArgumentParser.Services.Parsers;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var parser = host.Services.GetRequiredService<IParser<Dictionary<string, string>>>();
        
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole();
        });

        ILogger logger = loggerFactory.CreateLogger<Program>();

        var parsed = parser.Parse(args);
        logger.LogInformation("Parsed arguments: {Arguments}", parsed);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddScoped<IParser<Dictionary<string, string>>, ArgumentParser>();
                services.AddScoped<IParser<Arguments>, FixedArgumentParser<Arguments>>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options => options.IncludeScopes = true);
                logging.AddEventLog();
            });
    }