using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Tsubasa.Commands.Services;
using Tsubasa.Commands.Services.CommandServices;
using Tsubasa.Components;
using Tsubasa.Configuration;

namespace Tsubasa.Services
{
    public static class ServiceManager
    {
        /// <summary>
        /// Build a service provider
        /// </summary>
        /// <param name="clientConfiguration"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(ClientConfiguration clientConfiguration)
        {
            return new ServiceCollection()
                .AddSingleton(new Tsubasa(clientConfiguration)) //Add Discord Client
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .AddSingleton<DiscordEventHandlerService>()
                .AddSingleton<EmbedHelperService>()
                .AddSingleton<GeneralCommandService>()
                .BuildServiceProvider();
        }
    }
}