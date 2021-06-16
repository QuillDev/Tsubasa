using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Tsubasa.Commands.Services
{
    public class EmbedHelperService
    {
        private readonly Tsubasa _client;

        //Embed colors
        private static readonly Color NormalEmbedColor = new(194, 66, 237);
        private static readonly Color ErrorEmbedColor = new(235, 59, 76);

        public EmbedHelperService(Tsubasa client)
        {
            _client = client;
        }

        /// <summary>
        /// Get a normal embed with the given data
        /// </summary>
        /// <param name="msg">to get author data from</param>
        /// <param name="title">to set on the embed</param>
        /// <param name="description">to set on the embed</param>
        /// <returns></returns>
        public async Task<Embed> GetEmbed(SocketMessage msg, string title, string description)
        {
            return await GetBasicEmbedBuilder(
                msg,
                title,
                description,
                NormalEmbedColor
            );
        }

        /// <summary>
        /// Get an error embed for the given data
        /// </summary>
        /// <param name="msg">to get user data from</param>
        /// <param name="title">of the embed</param>
        /// <param name="description">of the embed</param>
        /// <returns></returns>
        public async Task<Embed> GetError(SocketMessage msg, string title, string description)
        {
            return await GetBasicEmbedBuilder(
                msg,
                title,
                $"{description}\nLook like a bug? Report it here https://github.com/QuillDev/Tsubasa/issues/new",
                ErrorEmbedColor
            );
        }

        /// <summary>
        /// Build a basic embed that we can inject data from at higher levels
        /// </summary>
        /// <param name="msg">to get author data from</param>
        /// <param name="title">to set on the embed</param>
        /// <param name="description">to set on the embed</param>
        /// <param name="color">to set the embed color to</param>
        /// <returns></returns>
        private async Task<Embed> GetBasicEmbedBuilder(SocketMessage msg, string title, string description, Color color)
        {
            return await Task.Run(() => new EmbedBuilder()
                .WithAuthor(_client.CurrentUser.Username, _client.CurrentUser.GetAvatarUrl())
                .WithTitle(title)
                .WithDescription(description)
                .WithFooter($"Requested by: {msg.Author.Username}", msg.Author.GetAvatarUrl())
                .WithColor(NormalEmbedColor)
                .WithCurrentTimestamp()
                .Build()
            ).ConfigureAwait(false);
        }
    }
}