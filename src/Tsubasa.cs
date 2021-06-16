using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Tsubasa.Configuration;

namespace Tsubasa
{
    public class Tsubasa : DiscordShardedClient
    {
        public ClientConfiguration ClientConfiguration { get; }

        public Tsubasa(ClientConfiguration clientConfig)
            : base(clientConfig.ShardIds, BuildSocketConfig(clientConfig))
        {
            //Set member vars
            ClientConfiguration = clientConfig;

            //Register any listeners
            ShardReady += OnLogin;
        }

        /// <summary>
        /// Build the socket config for the base client based on our tsubasa configuration
        /// </summary>
        /// <param name="clientConfig">to build configuration from</param>
        /// <returns>a fully configured socket config</returns>
        private static DiscordSocketConfig BuildSocketConfig(ClientConfiguration clientConfig)
        {
            return new()
            {
                MessageCacheSize = clientConfig.MessageCacheSize,
                LogLevel = LogSeverity.Verbose,
                TotalShards = clientConfig.ShardIds.Length
            };
        }

        private static async Task OnLogin(DiscordSocketClient client)
        {
            await Task.Run(() => { Console.WriteLine($"Client logged in as {client.CurrentUser.Username}"); }).ConfigureAwait(false);
        }
    }
}