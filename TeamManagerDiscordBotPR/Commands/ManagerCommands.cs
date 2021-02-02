using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;
using TeamManagerDiscordBotPR.Attributes;


// specific commands that are for designated for management and staff
// current problems: some commands "hang" or have delays in between sending some messages. Related to deadlocks?

namespace TeamManagerDiscordBotPR.Commands
{
    public class ManagerCommands : BaseCommandModule

    {
        [Command("weekly")]
        [Description("post weekly schedule for the upcoming week. Set up for 3 days in advanced.")]                                                                                             
        [RequireRoles(RoleCheckMode.Any, "Manager", "Asst Manager", "Team Creator", "Team Captain")]                                     //Checks for a user with one of these discord roles
        public async Task Weekly(CommandContext ctx)
        {
            //get rid of delay??
            //add where emojis are already included
            //auto post every friday

            await ctx.Channel.SendMessageAsync("<@&[PlayerRoleID]]> <@&[CoachRoleID]> Please react for scheduling:\n" +                  //[PlayerRoleID] and [CoachRoleID] should be replaced with your discord's specific id for the role
                "Tank = :shield: DPS = :gun: Support = :syringe:");                                                                      //temporarily using these global emojis until I learn how to auto embed my own
            for (int i = 3; i < 10; i++)                                                                                                 //starts the loop 3 days ahead of current day and loops 7 times (days of the week)
            {
                DayOfWeek dayName = DateTime.Today.AddDays(i).DayOfWeek;                                                                 //Uses DateTime to store specific day of the week, day number, and month number
                string monthNum = DateTime.Today.AddDays(i).Month.ToString();                                                            //posts the date of the next seven days (3 days ahead).
                string dayNum = DateTime.Today.AddDays(i).Day.ToString();                                                                //The idea is to post Friday night to give people a couple days to react (Mon-Sun) is our week.
                    
                await ctx.Channel.SendMessageAsync("React if you can vod review/scrim "                                                  //wish there was a way to convert to specific time zones and post in people's 
                    + dayName + " [" + monthNum + "/" + dayNum + "] between 9-11pm EST*");
            }
            await ctx.Channel.SendMessageAsync("*8-10pm CST, 7-9pm MST, 6-8pm PST").ConfigureAwait(false);

        }


        //post roster and staff
        [Command("roster")]
        [Description("posts team roster")]
        [RequireRoles(RoleCheckMode.Any, "Manager", "Asst Manager", "Team Creator", "Team Captain")]            //Checks for a user with one of these discord roles

        //idea behind this is to include all staff, current roster players, inactives, and former players
        //delete whatever you don't need. Formatted for easy reading
        //Insert people's discord ID inside <@>
        public async Task Roster(CommandContext ctx)
        {
            //posts the current date that the command is used to let people know when roster updated

            var month = DateTime.Today.Month.ToString();                                        //converts current month to string and stores in month variable
            var day = DateTime.Today.Day.ToString();                                            //converts current day to string and stores in day variable
            var year = DateTime.Today.Year.ToString();                                          //converts current year to string and stores in string variable

            await ctx.Channel.SendMessageAsync("\n--------------------------------------" +
                "\nUpdated as of: **__" + month + "/" + day + "/" + year + "__**" +             //Date in the form of Month/Day/Year
                "\n**__Staff__** " +                                                            //Staff (coaches and management)
                "\nCoach: <@>" +                                                                //Input your coaches discord ID
                "\nManager: <@>" +                                                              //Input manager discord ID
                "\nAsst Manager: <@>" +                                                         //Input assistant manager discord ID
                "\n--------------------------------------").ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync("\n:shield: **__Tanks__** :shield: " +           //Tank section
                "\nMain Tank: <@>" +                                                            //Tank1 Discord ID
                "\nOff Tank: <@>" +                                                             //Tank2 Discord ID
                "\nTank: [Open]" +                                                              //Tank3 Discord ID
                "\n--------------------------------------").ConfigureAwait(false);


            await ctx.Channel.SendMessageAsync("\n:gun: **__DPS__** :rocket: " +                //DPS Section
                "\nHitscan DPS: <@>" +                                                          //DPS1 Discord ID
                "\nProjectile DPS: <@>" +                                                       //DPS2 Discord ID
                "\nFlex DPS: <@>" +                                                             //DPS3 Discord ID
                "\n--------------------------------------").ConfigureAwait(false);


            await ctx.Channel.SendMessageAsync("\n:syringe: **__Supports__** :syringe: " +      //Support Section
                "\nMain Support: <@>" +                                                         //Support Discord ID
                "\nOff Support: <@>" +                                                          //Support Discord ID
                "\nFlex Support: <@>" +                                                         //Support Discord ID
                "\n--------------------------------------").ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync("\n**__Inactive__**" +
                "\nOff Tank: <@" +                                                              //Inactive/Sub ID
                "\n--------------------------------------").ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync("**__Former Players__**" +                       //past players
                "\nFormer: <@>" +                                                               //former player ID

                "\n--------------------------------------").ConfigureAwait(false);

        }

        //invite player to join team by user id. Work in progress.

        /*
        [Command("invite")]
        [Description("invites person to team")]
        [RequireRoles(RoleCheckMode.Any, "Manager", "Team Owner", "Team Captain")]

        public async Task invite(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Would you like to join our team?",
                ImageUrl = ctx.Client.CurrentUser.AvatarUrl,        //ImageURL and not AvatarURL(doesn't work anymore)
                Color = DiscordColor.Green,
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMessage &&
                x.User == ctx.User &&
                (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);

            if(reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = ctx.Guild.GetRole(794060240084795472);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if(reactionResult.Result.Emoji == thumbsDownEmoji)
            {
                //do nothing
            }
            else
            {
                //something glubbed up
            }

            await joinMessage.DeleteAsync().ConfigureAwait(false);*/

        // This isn't a command we use currently for our team, but this was to post scrimmages that were confirmed in a separate channel for organization
        // as to not clutter up the scheduling channel itself and to let people know the information of the upcoming confirmed scrimmage
        // The command itself is kind of messy and has too many parameters. Could use some improvements.

        [Command("confirmed")]
        [Description("confirmed scrims")]
        [RequireRoles(RoleCheckMode.Any, "Manager", "Asst Manager", "Team Creator", "Team Captain")]        //requires someone with at least one of these roles
        [RequireChannel(ChannelCheckMode.Any, "confirmed-scrims")]                                          //confined to specific channel
        public async Task Confirmed(CommandContext ctx, int startTime, string teamName, double teamSR,      //format comes out nice, but too many parameters
            string battleTag, string discordName)
        {
            DayOfWeek currentDay = DateTime.Today.DayOfWeek;
            string monthNum = DateTime.Today.Month.ToString();
            string dayNum = DateTime.Today.Day.ToString();
            int finishTime = startTime + 2;                                                                 //add 2 hours to start time (2 hr scrim blocks)

            await ctx.Channel.SendMessageAsync("<@&> <@&>\n" +                                              //once again all ids need to be specific to your discord server
                "**__ " + currentDay + " " + monthNum + "/" + dayNum + "__**\n" +
                startTime + "-" + finishTime + "pm EST\n" +
                teamName + " | " + teamSR + "k\n" +
                "BT: " + battleTag + "\n" +
                "Discord: " + discordName).ConfigureAwait(false);

            //full proof it later....number ranges. Space in team name? Can't leave blank? 
        }

        //Basically when I'm creating a new command, make it here for testing purposes
        [Command("test")]
        [Description("test")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("test");
        }

    }
}
