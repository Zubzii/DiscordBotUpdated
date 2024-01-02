using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;

namespace Discord_Bot_v1
{
    public class Class2 : BaseCommandModule
    {
        [Command("Innovator")]
        public async Task Innovator(CommandContext ctx) //Creates Ping, an async task that takes in some argument
        {
            var joinEmbed = new DiscordEmbedBuilder()
            {
                Title = "Want to be an Innovator?",
                Color = DiscordColor.Red
                
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed);

            var thumbsUp = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDown = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUp);
            await joinMessage.CreateReactionAsync(thumbsDown);

            var interactivity = ctx.Client.GetInteractivity();

            var result = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMessage &&
                x.User == ctx.User &&
                (x.Emoji == thumbsUp || x.Emoji == thumbsDown));

            if(result.Result.Emoji == thumbsUp)
            {
                var role = ctx.Guild.GetRole(1189659282866712688);
                await ctx.Member.GrantRoleAsync(role);

            }

            await joinMessage.DeleteAsync();
        }

    }
}
