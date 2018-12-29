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
        //the !info command
        [Command("info"), Summary("Info command")]
        public async Task info()
        {
            //Makes the Embed
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Info");
            Embed.WithDescription("Hi I am CodingBot\n"
                + "I can help you with:");
            Embed.WithColor(0, 128, 255);
            Embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/498614023168720916/506142446246166538/-.jpg");
            Embed.AddField("!info", "Gives you a list of commands i support.");
            Embed.AddField("!spam, !spam me", "Returns the message amount you have.");
            Embed.AddField("!...", "Some description");
            Embed.AddField("!...", "Some description");
            Embed.WithFooter("Made by sami119 and theBug"); ;
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
