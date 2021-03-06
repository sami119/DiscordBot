﻿using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using CodingBot.Resources.Database;
using System.Collections.Generic;

namespace CodingBot.Core.Commands
{
    public class SpamCommands : ModuleBase<SocketCommandContext>
    {
        //The Spam group
        [Group("spam"), Summary("Group to manage the spam commands")]
        public class SpamGroup : ModuleBase<SocketCommandContext>
        {
            //spam info command
            [Command("info"), Summary("Returns the spam info")]
            public async Task Info()
            {
                //Makes the Embed
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Spam Info");
                Embed.WithDescription("Hi I am CodingBot\n"
                    + "This is a list of my spam commands");
                Embed.WithColor(0, 128, 255);
                Embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/498614023168720916/506142446246166538/-.jpg");
                Embed.AddField("!spam, !spam me", "Returns the amount of messages you sent.");
                Embed.AddField("!spam username", "Returns how many messages given User sent");
                Embed.AddField("!spam leaderboard", "Returns the top 3 spamers");
                Embed.AddField("!spam res username, !spam reset username", "Resets the amount of messages you sent. | Admin command");
                Embed.WithFooter("Made by sami119 and theBug"); ;
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }

            //spam me, spam command
            [Command(""), Alias("me"), Summary("Returns how many messages i sent")]
            public async Task Me()
            {
                await Context.Channel.SendMessageAsync($"Hey {Context.User.Mention}, we noticed that you have spamed {Data.Data.GetMessagesAmount(Context.User.Id)} messages");
            }

            //spam username command
            [Command(""), Summary("Returns how many messages given User sent")]
            public async Task User(SocketUser user)
            {
                //tries for exeptions
                try
                {
                    //Checks if the user is bot
                    if (user.IsBot)
                    {
                        await Context.Channel.SendMessageAsync(":x: Bots messages are not caunted!");
                        return;
                    }

                    //Execute
                    await Context.Channel.SendMessageAsync($"User {user.Username} has sended {Data.Data.GetMessagesAmount(user.Id)} messages");
                }
                catch
                {
                    await Context.Channel.SendMessageAsync("Sorry there is a problem with that command. We will fix it!");
                }
            }

            //spam reset, spam res command
            [RequireUserPermission(GuildPermission.Administrator)]
            [Command("reset"), Alias("res"), Summary("Resets the database")]
            public async Task Res(IUser user = null)
            {
                //Checks
                if(user == null)
                {
                    await Context.Channel.SendMessageAsync(":x: You need to tell me which user's data you want to reset!");
                    return;
                }

                if (user.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Bots can't be reset!");
                    return;
                }

                //Execution
                await Context.Channel.SendMessageAsync($"{user.Mention}, your message amount has been reset by {Context.User.Username}!");

                //Save the Data
                using(var DbContext = new SqliteDbContext())
                {
                    DbContext.spam.RemoveRange(DbContext.spam.Where(x=>x.UserId == user.Id));
                    await DbContext.SaveChangesAsync();
                }
            }

            //spam leaderboard command
            [Command("leaderboard"), Summary("Returns the top 3 spamers")]
            public async Task Leaderboard()
            {
                //Initializes the data of the database
                List<Spam> spam = Data.Data.GetTop3Spams();
                Spam firstSpam = spam[0];
                Spam secondSpam = spam[1];
                Spam thirdSpam = spam[2];

                //Makes the embed and executes
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Spam LeaderBoard");
                Embed.WithDescription("Here is the list of top 3 spammers");
                Embed.WithColor(0, 128, 255);
                Embed.AddField($"1st Spammer - {firstSpam.Name}", $"with {firstSpam.MessagesSend} messages sent.");
                Embed.AddField($"2nd Spammer - {secondSpam.Name}", $"with {secondSpam.MessagesSend} messages sent.");
                Embed.AddField($"3rd Spammer - {thirdSpam.Name}", $"with {thirdSpam.MessagesSend} messages sent.");
                Embed.WithFooter("Made by sami119 and theBug");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }
    }
}
