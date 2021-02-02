using DiscordBot;

namespace TeamManagerDiscordBotPR
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();                                //creates instance of bot
            bot.RunAsync().GetAwaiter().GetResult();            //runs asynchronously after confirming bot token
        }
    }
}
