using System.Threading.Tasks;
using Discord.WebSocket;

namespace Tsubasa.Components
{
    public class DiscordEventHandlerService
    {
        private readonly Tsubasa _client;
        private readonly CommandHandlerService _commandHandlerService;

        public DiscordEventHandlerService(Tsubasa client, CommandHandlerService commandHandlerService)
        {
            _client = client;
            _commandHandlerService = commandHandlerService;
        }

        public void Configure()
        {
            _client.MessageReceived += DiscordMessageReceived;
        }

        private async Task DiscordMessageReceived(SocketMessage msg)
        {
            if (msg.Author.IsBot) return;
            await _commandHandlerService.HandleCommandAsync(msg);
        }
    }
}