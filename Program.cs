using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Tsubasa.Configuration;
using Tsubasa.Services;

namespace Tsubasa
{
    internal static class Program
    {

        private static void Main(string[] args)
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
    }
}