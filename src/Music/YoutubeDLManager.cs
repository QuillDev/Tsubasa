using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Tsubasa.Music
{
    public static class YoutubeDlManager
    {
        private const string YoutubeDlUrl = "https://yt-dl.org/downloads/latest/youtube-dl";
        private const string YoutubeDlPath = "./bin/youtube-dl";

        //TODO: make it automatically update somehow
        /// <summary>
        /// Check if youtube-dl is installed and if it isn't install the latest version online
        /// </summary>
        public static async Task CheckYoutubeDl()
        {
            //Create a bin folder if it doesn't exist
            Directory.CreateDirectory("./bin");

            if (File.Exists(YoutubeDlPath))
            {
                Console.WriteLine("Found youtube-dl, starting client");
                return;
            }

            await Task.Run(async () =>
            {
                //If the OS is Windows use curl
                if (OperatingSystem.IsWindows())
                {
                    using var process = Process.Start(
                        new ProcessStartInfo
                        {
                            FileName = "curl",
                            Arguments = $"-L {YoutubeDlUrl} -o {YoutubeDlPath}",
                            RedirectStandardOutput = false,
                        });

                    if (process == null)
                    {
                        Console.WriteLine("Could not find curl... How?");
                        return;
                    }

                    await process.WaitForExitAsync();

                    Console.WriteLine("Successfully downloaded youtube-dl");
                    return;
                }

                Console.WriteLine("OS's not on windows are not yet supported!");
            });
        }
    }
}