using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Tsubasa.Configuration;

namespace Tsubasa
{
    class Program
    {
        private static readonly int[] ShardIds = {0};

        private static void Main(string[] args)
        {
            new Program().RunAsync().GetAwaiter().GetResult();
        }

        private async Task RunAsync()
        {
            var configManager = new ConfigManager();
            var config = await configManager.LoadConfig(); //Load the config from the config manager

            var services = GetServiceProvider(config); //get the service provider

            //if the client service is bugging, just close the program
            var client = services.GetService<Tsubasa>();
            if (client == null)
            {
                Console.WriteLine("Client service does not exist!");
                return;
            }

            await client.LoginAsync(TokenType.Bot, config.Token); //Log the client into discord

            //Configure an infinite delay since we're a server
            await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
        }

        IServiceProvider GetServiceProvider(ClientConfiguration clientConfiguration)
        {
            return new ServiceCollection()
                .AddSingleton(new Tsubasa(clientConfiguration.ShardIds, new DiscordSocketConfig
                {
                    MessageCacheSize = clientConfiguration.MessageCacheSize,
                    AlwaysDownloadUsers = false, //TODO: see if needed for some other feature later
                    LogLevel = LogSeverity.Verbose,
                    TotalShards = clientConfiguration.ShardIds.Length
                    
                })) //Add Discord Client
                .BuildServiceProvider();
        }
    }
}