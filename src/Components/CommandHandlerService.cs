using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Tsubasa.Configuration;

namespace Tsubasa.Components
{
    public class CommandHandlerService
    {
        private readonly Tsubasa _client;
        private readonly ClientConfiguration _clientConfig;
        private readonly IServiceProvider _services;
        private CommandService _commandService;

        public CommandHandlerService(Tsubasa client, CommandService commandService, IServiceProvider services)
        {
            _client = client;

            _services = services;
            _commandService = commandService;
            _clientConfig = client.ClientConfiguration;
        }

        public async Task ConfigureAsync()
        {
            _commandService = new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Verbose
            });

            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        /// <summary>
        /// Handle the inputted command asynchronously and find matching commands
        /// </summary>
        /// <param name="arg"></param>
        public async Task HandleCommandAsync(SocketMessage arg)
        {
            //If the arg is not a socket user message, just return out
            if (arg is not SocketUserMessage msg)
            {
                return;
            }

            var argPos = 0;
            //Check if the message has none of the possible prefixes
            if (
                !msg.HasMentionPrefix(_client.CurrentUser, ref argPos)
                && !msg.HasStringPrefix(_clientConfig.Prefix, ref argPos)
                && !msg.HasStringPrefix(_clientConfig.Prefix.ToUpper(), ref argPos)
            )
            {
                return;
            }

            var context = new ShardedCommandContext(_client, msg);

            await _commandService.ExecuteAsync(context, argPos, _services);
        }
    }
}