using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Tsubasa
{
    public class Tsubasa : DiscordShardedClient
    {
        public Tsubasa(int[] shardIds, DiscordSocketConfig discordSocketConfig) : base(shardIds, discordSocketConfig)
        {
            LoggedIn += OnLogin;
        }

        private static async Task OnLogin()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Client logged in");
            });
        }
    }
}