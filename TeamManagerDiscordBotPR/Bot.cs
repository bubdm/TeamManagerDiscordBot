using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using TeamManagerDiscordBotPR;


namespace DiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }

        public InteractivityExtension Interactivity { get; private set; }

        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))                           //file config.json contains bot token
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,             // uses prefix instead of having to @bot
                DmHelp = true,                          // The help commands are dmed to the user
                //EnableDefaultHelp = false,            // possibly create my own help section eventually. Turns off default with false.
                UseDefaultCommandHandler = true,        // possibly turn off later
            };

            Commands = Client.UseCommandsNext(commandsConfig);


            await Client.ConnectAsync();

            await Task.Delay(-1);                               //keeps the bot running until terminated
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
