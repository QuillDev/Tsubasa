using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Tsubasa.Commands.Services.CommandServices
{
    public class GeneralCommandService : TsubasaCommandService
    {
        public GeneralCommandService(EmbedHelperService embedHelperService) : base(embedHelperService)
        {
        }

        public Task<Embed> HelpCommand(SocketMessage msg)
        {
            return EmbedHelperService.GetEmbed(msg,
                "Tsubasa - Help",
                "Need Help?\nCheck out the official site: https://quill.moe/tsubasa");
        }
    }
}