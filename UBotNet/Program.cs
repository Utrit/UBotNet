using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;

internal class Program
{
    private static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureDiscordHost((context, config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug,
                    AlwaysDownloadUsers = false,
                    MessageCacheSize = 0,
                    GatewayIntents = GatewayIntents.Guilds
                };

                string? token = context.Configuration["Token"];

                if (string.IsNullOrEmpty(token))
                    throw new ArgumentNullException(nameof(token), "Token is null or empty. Specify it in your config.");

                config.Token = token;
            })
            .UseInteractionService((context, config) =>
            {
                config.LogLevel = LogSeverity.Debug;
                config.UseCompiledLambda = true;
            })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<InteractionHandler>();
                services.AddSingleton<IConfigurationService>(provider =>
                {
                    return new ConfigurationService(context.Configuration);
                });
            })
            .Build();

        await host.RunAsync();
    }
}