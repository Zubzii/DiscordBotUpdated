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
    public class Class1 : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns thumbs up")]
        public async Task Ping(CommandContext ctx) //Creates Ping, an async task that takes in some argument
        {
            await ctx.Channel.SendMessageAsync(":+1:").ConfigureAwait(false); //sends thumbs up when "ping" command is sent
        }

        [Command("add")]
        [Description("Adds two integers; input as as two integers seperated by a space")]
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo) //Creates add , an async task that takes in 2 ints and adds them
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString()).ConfigureAwait(false); //sends pong when "ping" command is sent
        }

        [Command("response")]

        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel);
            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("Poll")]
        [Description("Creates a poll")]
        public async Task Poll(CommandContext ctx, string pollName, string option1, string option2, string option3, string option4, string option5) 
        {
            var Poll = new DiscordEmbedBuilder

            {
                Title = pollName,
                Description = "Vote Once!"

            };

            Poll.AddField("Option 1",option1);
            Poll.AddField("Option 2", option2);
            Poll.AddField("Option 3", option3);
            Poll.AddField("Option 4", option4);
            Poll.AddField("Option 5", option5);

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: Poll).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var numberOne = DiscordEmoji.FromName(ctx.Client, ":one:");
            var numberTwo = DiscordEmoji.FromName(ctx.Client, ":two:");
            var numberThree = DiscordEmoji.FromName(ctx.Client, ":three:");
            var numberFour = DiscordEmoji.FromName(ctx.Client, ":four:");
            var numberFive = DiscordEmoji.FromName(ctx.Client, ":five:");

            await pollMessage.CreateReactionAsync(numberOne);
            await pollMessage.CreateReactionAsync(numberTwo);
            await pollMessage.CreateReactionAsync(numberThree);
            await pollMessage.CreateReactionAsync(numberFour);
            await pollMessage.CreateReactionAsync(numberFive);
        }

        [Command("DynamicPoll")]
        [Description("Creates a poll")]
        public async Task DynamicPoll(CommandContext ctx, string pollName, params string[] options)
        {
            var Poll = new DiscordEmbedBuilder

            {
                Title = pollName,
                Description = ":exclamation: Vote Once! :exclamation:"
            };

            var x = 1;

            foreach (var option in options)
            {
                Poll.AddField("Option " + x, option);
                x += 1;
            }

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: Poll).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var number1 = DiscordEmoji.FromName(ctx.Client, ":one:");
            var number2 = DiscordEmoji.FromName(ctx.Client, ":two:");
            var number3 = DiscordEmoji.FromName(ctx.Client, ":three:");
            var number4 = DiscordEmoji.FromName(ctx.Client, ":four:");
            var number5 = DiscordEmoji.FromName(ctx.Client, ":five:");

            var emojis = new List<DiscordEmoji> { number1, number2, number3, number4, number5 };

            var y = 0;

            foreach (var option in options)
            {
                await pollMessage.CreateReactionAsync(emojis[y]);
                y += 1;
            }

        }
    }
}
