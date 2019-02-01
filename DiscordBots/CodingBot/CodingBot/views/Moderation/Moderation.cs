using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using CodingBot.Resources.Datatypes;

namespace CodingBot.Core.Moderation
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireOwner()]
        [Command("reload"), Summary("Reload the settings.json file while the bot is running")]
        public async Task Reload()
        {
            //Checks
            string SettingsLocation = @"D:\My-Zone\IT-Zone\Repos\Projects\DiscordBots\CodingBot\CodingBot\Data\Settings.json";
            if (!File.Exists(SettingsLocation))
            {
                await Context.Channel.SendMessageAsync(":x: The file is not found in the given location. The expected location can be found in the log!");
                Console.WriteLine(SettingsLocation);
                return;
            }

            //Execution
            string JSON = "";
            using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            using (var ReadSettings = new StreamReader(Stream))
            {
                JSON = ReadSettings.ReadToEnd();
            }

            Settings settings = JsonConvert.DeserializeObject<Settings>(JSON);

            //Save
            ESettings.banned = settings.banned;
            ESettings.log = settings.log;
            ESettings.owner = settings.owner;
            ESettings.token = settings.token;
            ESettings.version = settings.version;

            await Context.Channel.SendMessageAsync(":white_check_mark: All settings were updated succesfully!");
        }

        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Command("kick", RunMode = RunMode.Async), Summary("Kicks the given user.")]
        public async Task Kick(SocketGuildUser user, [Remainder] string reason)
        {
            //Checks
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: You cannot kick bot!");
                return;
            }

            //Execute
            await Context.Guild.GetTextChannel(528599481508167695).SendMessageAsync($"{user.Username} was kicked by {User1.Username} {reason}");
            await user.KickAsync(reason);
        }
    }
}
