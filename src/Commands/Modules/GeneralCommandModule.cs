using System.Threading.Tasks;
using Discord.Commands;
using Tsubasa.Commands.Services;
using Tsubasa.Commands.Services.CommandServices;

namespace Tsubasa.Commands.Modules
{
    public class GeneralCommandModule : ModuleBase<ShardedCommandContext>
    {
        private readonly GeneralCommandService _commandService;

        public GeneralCommandModule(GeneralCommandService commandService)
        {
            _commandService = commandService;
        }

        [Command("help")]
        public async Task HelpCommand()
        {
            await ReplyAsync(embed: await _commandService.HelpCommand(Context.Message));
        }
    }
}