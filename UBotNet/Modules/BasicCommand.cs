﻿using Discord;
using Discord.Interactions;

namespace UBotNet.Modules;

public class BasicCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("latency", "Display bot latency")]
    public async Task LatencyAsync()
    {
        int latency = Context.Client.Latency;

        var embed = new EmbedBuilder()
            .WithTitle("Latency")
            .WithDescription($"{latency} ms")
            .WithColor(Color.Blue)
            .Build();

        await RespondAsync(embed: embed,ephemeral:true);
    }

    [SlashCommand("avatar", "Get a user avatar")]
    public async Task AvatarAsync(
        [Summary("user", "The user to get avatar")] IUser? user = null)
    {
        // If user was not specified or it is null, replace it with interaction executor.
        //user ??= Context.User;

        // Build an embed to respond.
        var embed = new EmbedBuilder()
            .WithTitle($"{user.Username}#{user.Discriminator}")
            .WithImageUrl(user.GetAvatarUrl(size: 4096) ?? user.GetDefaultAvatarUrl())
            .WithColor(Color.Blue)
            .Build();

        // Respond to the interaction.
        await RespondAsync(embed: embed);
    }
}