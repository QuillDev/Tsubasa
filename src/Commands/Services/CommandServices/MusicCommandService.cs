using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Microsoft.VisualBasic;
using Tsubasa.Music;

namespace Tsubasa.Commands.Services.CommandServices
{
    public class MusicCommandService
    {
        private Tsubasa _client;
        private readonly Dictionary<ulong, IAudioClient> _dispatcherDictionary = new();

        private readonly EmbedHelperService _embedHelper;

        public MusicCommandService(Tsubasa client, EmbedHelperService embedHelper)
        {
            _client = client;
            _embedHelper = embedHelper;
        }

        public async Task<Embed> Play(SocketUser user, SocketUserMessage msg, string[] args)
        {
            if (user is not IGuildUser guildUser)
            {
                return await _embedHelper.GetEmbed(msg, "Tsubasa - Play", "User is not connected to a voice channel.");
            }

            var guild = guildUser.Guild;

            IAudioClient dispatcher;
            if (!_dispatcherDictionary.ContainsKey(guild.Id))
            {
                await msg.ReplyAsync(embed: await _embedHelper.GetEmbed(msg, "Tsubasa - Play",
                    "This guild does not have a dispatcher, making one.")
                );

                dispatcher = await guildUser.VoiceChannel.ConnectAsync();
                _dispatcherDictionary.Add(guild.Id, dispatcher);
            }
            else
            {
                dispatcher = _dispatcherDictionary[guild.Id];
            }

            var ytdlStream = await YoutubeDlManager.DownloadVideoAudio("https://www.youtube.com/watch?v=pDFFEWIzau4");
            await using (var output = ytdlStream.StandardOutput.BaseStream)
            await using (var discord = dispatcher.CreatePCMStream(AudioApplication.Mixed))
            {
                try
                {
                    await output.CopyToAsync(discord);
                }
                finally
                {
                    await discord.FlushAsync();
                }
            }

            return await _embedHelper.GetEmbed(msg, "Tsubasa - Play", "Not yet implemented");
        }
    }
}