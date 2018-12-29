using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using CodingBot.Core.Data;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace CodingBot
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageRecieved;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Log += Client_Log;
            Client.Ready += Client_Ready;
            
            string Token = "";
            using (var Stream = new FileStream((Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(@"bin\Debug\netcoreapp2.1", @"Data\Token.txt"), FileMode.Open, FileAccess.Read))
            using (var ReadToken = new StreamReader(Stream))
            {
                Token = ReadToken.ReadToEnd();
            }

            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Looking for !info", null, StreamType.NotStreaming);
        }

        private async Task Client_MessageRecieved(SocketMessage msg)
        {
            var Message = msg as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing command. Text: {Context.Message.Content} | Error {Result.ErrorReason}");
            }
        }

        //Work on this
        private async Task Save(SocketMessage msg)
        {
            var Message = msg as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            await Data.SaveMessagesAmount(Context.User.Id, 1);
        }
    }
}
