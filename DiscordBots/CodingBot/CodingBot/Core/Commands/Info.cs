using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace CodingBot.Core.Commands
{
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Command("info"), Summary("Info command")]
        public async Task info()
        {
            await Context.Channel.SendMessageAsync("Hello I am CodingBot\nI was created by Sami119 and theBug\nHere is list of my commands\n!info - displays info of all my commands");
        }

        [Command("embed"), Summary("Test command")]
        public async Task Embed()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"{Context.User}", Context.User.GetAvatarUrl());
            Embed.WithColor(40, 200, 150);
            Embed.WithFooter("The Footer", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("A dummy description.\n"
                + "[search for info](https://www.google.com/)");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
