using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace TeamManagerDiscordBotPR.Commands
{
    public class FunCommands : BaseCommandModule
    {
        //simple basic discord bot beginner command. Basically the hello world of discord bots

        [Command("ping")]
        [Description("returns pong")]
        public async Task ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong!").ConfigureAwait(false);
        }

        //praise the bot and it posts a smiley face...
        //expanding on this later
        [Command("goodbot")]
        [Description("praise the bot")]
        public async Task GoodBot(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(":D").ConfigureAwait(false);
        }

        //scold the bot and it posts a capital D colon face...
        //expanding on this later
        [Command("badbot")]
        [Description("scold the bot")]
        public async Task BadBot(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("D:").ConfigureAwait(false);
        }

        //does math. You input two numbers and it adds them together format: ?add num1 num2
        [Command("add")]
        [Description("adds two numbers")]
        public async Task Add(CommandContext ctx,
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);

        }


        //disabled til next holiday season                                  //posts gif of padoru meme
        /*
        [Command("Padoru")]
        [Description("Padoru padoru!!")]
        public async Task Padoru(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://gph.is/g/apVJ0ev")
                .ConfigureAwait(false);
        }
        */

        //the bot looks at the message that the user sent and repeats it in the same channel
        [Command("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        /*
         [Command("poll")]
         public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
         {
             var interactivity = ctx.Client.GetInteractivity();
             var options = emojiOptions.Select(x => x.ToString());

             var pollEmbed = new DiscordEmbedBuilder
             {
                 Title = "Poll",
                 Description = string.Join(" ", options)
             };

             var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

             foreach(var option in emojiOptions)
             {
                 await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
             }
             var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
             var distinctResult = result.Distinct();

             var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

             await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
         } */                //broken poll command...it technically works, but some aspects are awkward and needs tweaking.




    }
}
