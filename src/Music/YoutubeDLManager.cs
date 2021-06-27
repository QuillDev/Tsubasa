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

        public static async Task DownloadVideoAudio(string url)
        {
            // Example command youtube-dl https://youtu.be/y2XArpEcygc --extract-audio --audio-format mp3

            await Task.Run(() =>
            {
                if (OperatingSystem.IsWindows())
                {
                    using var process = Process.Start(
                        new ProcessStartInfo
                        {
                            FileName = YoutubeDlPath,
                            Arguments = $"{url} --extract-audio --audio-format mp3",
                            RedirectStandardOutput = true
                        });

                    //TODO: Add custom exceptions
                    if (process == null)
                    {
                        throw new NullReferenceException(
                            $"Issue when trying to download audio for url {url}! Process is null!");
                    }

                    while (!process.HasExited)
                    {
                        while (!process.StandardOutput.EndOfStream)
                        {
                            Console.WriteLine(process.StandardOutput.ReadLine());
                        }
                    }


                    process.WaitForExitAsync();
                }

                else if (OperatingSystem.IsLinux())
                {
                    Console.WriteLine("Linux is not yet implemented");
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Set file permissions to make sure youtube-dl is executable on linux machines
        /// </summary>
        private static async Task SetLinuxPermissions()
        {
            using var process = Process.Start(
                new ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"a+rx {YoutubeDlPath}"
                });


            //TODO: Throw Exceptions
            if (process == null)
            {
                Console.WriteLine("bash not found... again, how?");
                return;
            }

            await process.WaitForExitAsync().ConfigureAwait(false);
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

            //TODO: Add custom exceptions
            if (process == null)
            {
                throw new NullReferenceException($"error when trying to download YoutubeDL! Process is null!");
            }

            await process.WaitForExitAsync().ConfigureAwait(false);
        }
    }
}