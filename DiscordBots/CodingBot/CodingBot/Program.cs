using System;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

using CodingBot.Core.Data;
using CodingBot.Resources.Datatypes;

using Newtonsoft.Json;

namespace CodingBot
{
    class Program
    {
        //Client log
        private DiscordSocketClient Client;
        //Command log
        private CommandService Commands;

        //returns the main async func
        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        //Main async func used for Tasks
        private async Task MainAsync()
        {
            //Reads the json file
            string JSON = "";
            string SettingsLocation = @"D:\My-Zone\IT-Zone\Repos\Projects\DiscordBots\CodingBot\CodingBot\Data\Settings.json";
            using (var Stream = new FileStream(SettingsLocation, FileMode.Open, FileAccess.Read))
            using (var ReadSettings = new StreamReader(Stream))
            {
                JSON = ReadSettings.ReadToEnd();
            }
            Settings settings = JsonConvert.DeserializeObject<Settings>(JSON);

            //Saves the Json file into the ESetings class for future use
            ESettings.banned = settings.banned;
            ESettings.log = settings.log;
            ESettings.owner = settings.owner;
            ESettings.token = settings.token;
            ESettings.version = settings.version;

            //Initializing our Client
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                //LogLevel Config
                LogLevel = LogSeverity.Info
            });

            //Initializing our CommandService
            Commands = new CommandService(new CommandServiceConfig //CommandService Config
            {
                //Case Sensitive Commands
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            //Handles The recieved messages
            Client.MessageReceived += Client_MessageRecieved;

            //If is not here some of the commands can not work
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            //Handles the ClientLog
            Client.Log += Client_Log;

            //Handles the GameLog
            Client.Ready += Client_Ready;

            //Logs the client
            await Client.LoginAsync(TokenType.Bot, ESettings.token);

            //starts the conection
            await Client.StartAsync();

            //Prevent for not stoping
            await Task.Delay(-1);
        }

        //Here is the ClientLog Message
        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source} - {Message.Message}");
            //try
            //{
            //    SocketGuild guild = Client.Guilds.Where(x => x.Id == ESettings.log[0]).FirstOrDefault();
            //    SocketTextChannel channel = guild.Channels.Where(x => x.Id == ESettings.log[1]).FirstOrDefault() as SocketTextChannel;
            //    await channel.SendMessageAsync($"{DateTime.Now} at {Message.Source} - {Message.Message}");
            //}
            //catch{}
        }

        //Here is the GameLog Message
        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Looking for !info", null, StreamType.NotStreaming);
        }

        // Here is the MessageRecieved Log
        private async Task Client_MessageRecieved(SocketMessage msg)
        {

            //initializing the Message and the SocketCommandContext
            var Message = msg as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            //Cheks if the User is not bot and Saves the MessageAmmount
            if (!Context.User.IsBot)
            {
                Data.MessageAmmount++;
                await Data.SaveMessagesAmount(Context.User.Id, Data.MessageAmmount, Context.User.Username);
            }

            //Checs if the Message is null or the message content is "" and returns noting
            if (Context.Message == null || Context.Message.Content == "") return;

            //Cheks if the user is Bot and returns noting
            if (Context.User.IsBot) return;

            //Checs for messages with ! prefix or Mentions to the bot
            int ArgPos = 0;
            if (!(Message.HasStringPrefix("!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            //Returns the Result if is Succes
            var Result = await Commands.ExecuteAsync(Context, ArgPos);

            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands - Something went wrong with executing command | Text: {Context.Message.Content} | Error {Result.ErrorReason}");
            }
        }
    }
}
