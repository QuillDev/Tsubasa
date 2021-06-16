using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Tsubasa.Configuration
{
    public class ClientConfiguration
    {
        public string Token { get; set; }
        public int[] ShardIds { get; set; }
        public int MessageCacheSize { get; set; }

        public string Prefix { get; set; }

        /// <summary>
        /// Custom configuration of the client 
        /// </summary>
        /// <param name="token">of the client to login to</param>
        /// <param name="shardIds">Ids of shards for the client</param>
        /// <param name="messageCacheSize">max size of the message cache</param>
        /// <param name="prefix">the prefix for the bot</param>
        private ClientConfiguration(string token, int[] shardIds, int messageCacheSize, string prefix)
        {
            Token = token;
            ShardIds = shardIds;
            MessageCacheSize = messageCacheSize;
            Prefix = prefix;
        }

        /// <summary>
        /// Base configuration for a client
        /// </summary>
        public ClientConfiguration() : this("your_token_here", new[] {0}, 100, "t>")
        {
        }

        /// <summary>
        /// Serialize the given object to yaml
        /// </summary>
        /// <returns>the yaml string</returns>
        public string SerializeToYaml()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
            return serializer.Serialize(this);
        }
    }
}