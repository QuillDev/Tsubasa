using System;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Tsubasa.Configuration;

namespace Tsubasa.Services
{
    public class ServiceManager
    {
        /// <summary>
        /// Build a service provider
        /// </summary>
        /// <param name="clientConfiguration"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(ClientConfiguration clientConfiguration)
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