namespace Tsubasa.Commands.Services.CommandServices
{
    public abstract class TsubasaCommandService
    {
        protected readonly EmbedHelperService EmbedHelperService;

        protected TsubasaCommandService(EmbedHelperService embedHelperService)
        {
            EmbedHelperService = embedHelperService;
        }
    }
}