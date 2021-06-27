using System.Threading;
using System.Threading.Tasks;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Tsubasa.Components;
using Tsubasa.Configuration;
using Tsubasa.Music;
using Tsubasa.Services;

namespace Tsubasa
{
    internal static class Program
    {
        private static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Run the program in an asynchronous context so we can do all kinds of cool networking stuff
        /// </summary>
        private static async Task RunAsync()
        {
            var config = await ConfigManager.LoadConfig(); //Load the config from the config manager
            var services = ServiceManager.GetServiceProvider(config); //get the service provider

            //if the client service is bugging, just close the program
            var client = services.GetRequiredService<Tsubasa>();
            
            //Download youtube dl
            await YoutubeDlManager.CheckYoutubeDl();
            await YoutubeDlManager.DownloadVideoAudio("https://www.youtube.com/watch?v=G5hScSFkib4");
            

            await services.GetRequiredService<CommandHandlerService>().ConfigureAsync();
            services.GetRequiredService<DiscordEventHandlerService>().Configure();
            await client.LoginAsync(TokenType.Bot, config.Token); //Log the client into discord
            await client.StartAsync(); //start the bot

            //Configure an infinite delay since we're a server
            await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
        }
    }
}