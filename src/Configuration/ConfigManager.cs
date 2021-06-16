using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Tsubasa.Configuration
{
    public class ConfigManager
    {
        private const string ConfigDirPath = "./config/";
        private const string ConfigFilePath = ConfigDirPath + "client_config.yml";
        private static readonly StreamReader StreamReader = new(ConfigFilePath);

        //Create a deserializer
        private static readonly IDeserializer Deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();


        public async Task<ClientConfiguration> LoadConfig()
        {
            return await Task.Run(() =>
            {
                ClientConfiguration config;
                if (!File.Exists(ConfigFilePath))
                {
                    Console.WriteLine("Config file does not exist! Creating one from scratch");

                    //If the directory does not exist, create it
                    if (!Directory.Exists(ConfigDirPath))
                    {
                        Directory.CreateDirectory(ConfigDirPath);
                    }

                    //Set the config to a new client configuration
                    config = new ClientConfiguration();

                    //Create the file & Write to it
                    var file = File.CreateText(ConfigFilePath);
                    var writeTask = file.WriteAsync(config.SerializeToYaml());
                    Task.WaitAll(writeTask);
                    file.Close(); //Close the file stream
                    return config;
                }

                var contents = StreamReader.ReadToEndAsync().Result;
                config = Deserializer.Deserialize<ClientConfiguration>(contents);
                return config;
            });
        }
    }
}