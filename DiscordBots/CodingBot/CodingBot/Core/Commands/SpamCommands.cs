using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

namespace CodingBot.Core.Commands
{
    public class SpamCommands : ModuleBase<SocketCommandContext>
    {
        //spam me
        [Group("spam"), Summary("Group to manage the spam commands")]
        public class SpamGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("me"), Summary("Returns how many messages i sent")]
            public async Task Me()
            { 
                await Context.Channel.SendMessageAsync($"{Context.User}, your Message Amount is {Data.Data.GetMessagesAmount(Context.User.Id)}");
            }
        }
    }
}
