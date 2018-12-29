using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

namespace CodingBot.Core.Commands
{
    public class Speaking : ModuleBase<SocketCommandContext>
    {
        //hello command
        [Command("hello"), Alias("hi", "Hello", "Hi"), Summary("Says Hello")]
        public async Task Hello()
        {
            await Context.Channel.SendMessageAsync($"Hello {Context.User.Mention}");
        }
    }
}
