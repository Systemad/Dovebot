using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Dovebot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dovebot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += Log;

                var value = ConfigurationManager.AppSetting["DiscordToken"];
            
                await client.LoginAsync(TokenType.Bot, value);
                await client.StartAsync();

                await services.GetRequiredService<CommandHandlerService>().InitializeAsync();
            
                await Task.Delay(Timeout.Infinite);   
            }
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<PictureService>()
                .AddSingleton<UpcomingmatchService>()
                .BuildServiceProvider();
        }
        
        private static Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}