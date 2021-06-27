using System;
using System.Threading.Tasks;
using Discord.Commands;
using Tsubasa.Commands.Services.CommandServices;

namespace Tsubasa.Commands.Modules
{
    public class MusicCommandModule : ModuleBase<ShardedCommandContext>
    {
        private const string url = "https://www.youtube.com/watch?v=G5hScSFkib4";

        private readonly MusicCommandService _musicCommandService;

        public MusicCommandModule(MusicCommandService musicCommandService)
        {
            _musicCommandService = musicCommandService;
        }

        [Command("play"),]
        public async Task PlayCommand(params string[] args)
        {

            await ReplyAsync(embed: await _musicCommandService.Play(Context.User, Context.Message, args));
        }
    }
}