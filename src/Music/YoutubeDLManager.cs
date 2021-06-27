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
                if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
                {
                    await DownloadYoutubeDl();

                    //if the os is linux we need to change the permissions of the file
                    if (OperatingSystem.IsLinux())
                    {
                        await SetLinuxPermissions();
                    }

                    Console.WriteLine("Successfully downloaded youtube-dl");
                    return;
                }

                Console.WriteLine("Tsubasa currently only supports Linux and Windows!");
            });
        }

        private static async Task SetLinuxPermissions()
        {
            using var process = Process.Start(
                new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"chmod a+rx {YoutubeDlPath}"
                });

            //TODO: Throw Exceptions
            if (process == null)
            {
                Console.WriteLine("bash not found... again, how?");
                return;
            }

            await process.WaitForExitAsync();
        }

        /// <summary>
        /// Attempt to download the latest version of youtube-dl from their website
        /// </summary>
        private static async Task DownloadYoutubeDl()
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
        }
    }
}