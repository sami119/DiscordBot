using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingBot.Core.Data;
using CodingBot.Resources.Database;
using Discord.Commands;

namespace CodingBot.Core.Commands
{
    public class SpamCommands : ModuleBase<SocketCommandContext>
    {
        //The Spam group
        [Group("spam"), Summary("Group to manage the spam commands")]
        public class SpamGroup : ModuleBase<SocketCommandContext>
        {
            //spam me, spam command
            [Command(""), Alias("me"), Summary("Returns how many messages i sent")]
            public async Task Me()
            {
                await Context.Channel.SendMessageAsync($"Hey {Context.User}, we notice that you have spamed {Data.Data.GetMessagesAmount(Context.User.Id)} messages");
            }

            ////spam leaderboard command
            //[Command("leaderboard"), Summary("Returns the top 3 spamers")]
            //public async Task Leaderboard()
            //{
            //    await Context.Channel.SendMessageAsync("");
            //}
        }
    }
}
