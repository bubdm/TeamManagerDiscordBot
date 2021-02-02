using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

//this attribute was created to confine bot messages to specific channels
//Ex: if you want to use scheduling and only have it in a "scheduling" text channel
namespace TeamManagerDiscordBotPR.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RequireChannelAttribute : CheckBaseAttribute
    {
        public IReadOnlyList<string> ChannelNames { get; }
        public ChannelCheckMode CheckMode { get; }

        public RequireChannelAttribute(ChannelCheckMode checkMode, params string[] channelNames)
        {
            CheckMode = checkMode;
            ChannelNames = new ReadOnlyCollection<string>(channelNames);
        }
        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {


            if (ctx.Guild == null || ctx.Member == null)
            {
                return Task.FromResult(false);
            }

            bool contains = ChannelNames.Contains(ctx.Channel.Name, StringComparer.OrdinalIgnoreCase);

            return CheckMode switch
            {
                ChannelCheckMode.Any => Task.FromResult(contains),

                ChannelCheckMode.None => Task.FromResult(!contains),

                _ => Task.FromResult(false),
            };
        }
    }
}
