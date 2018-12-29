using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Discord.Commands;

using CodingBot.Resources.Datatypes;

namespace CodingBot.Core.Moderation
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("reload"), Summary("Reload the settings.json file while the bot is running")]
        public async Task Reload()
        {
            //Checks
            if(Context.User.Id != ESettings.owner)
            {
                await Context.Channel.SendMessageAsync(":x: You are not the owner.");
                return;
            }

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
    }
}
