using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Interactions.Builders;
using Microsoft.Extensions.Hosting;
using Service;

namespace UBotNet.Modules;

public class DebugCommands : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IConfigurationService _configuration;
    public DebugCommands(IConfigurationService configuration)
    {
        _configuration = configuration;
    }

    [SlashCommand("unloadslash","remove guild slash")]
    public async Task UnloadSlash()
    {
        var devId = _configuration.Configuration["DevId"];
        if (devId is null)
        {
            devId = "123";
        }
        if(Context.User.Id!=ulong.Parse(devId))
        {
            var errbed = new EmbedBuilder()
                .WithTitle("DEV COMMAND")
                .WithDescription($"only dev can execute this")
                .WithColor(Color.Red)
                .Build();
            await RespondAsync(embed: errbed,ephemeral:true);
            return;
        }
        List<ApplicationCommandProperties> applicationCommandProperties = new List<ApplicationCommandProperties>();
        await Context.Guild.BulkOverwriteApplicationCommandAsync(applicationCommandProperties.ToArray());
        var embed = new EmbedBuilder()
            .WithTitle("Work done")
            .WithDescription($"slash commands removed")
            .WithColor(Color.Green)
            .Build();

        await RespondAsync(embed: embed,ephemeral:true);
    }
}