using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Tsubasa.Configuration
{
    public class ClientConfiguration
    {
        public string Token { get; set; }
        public int[] ShardIds { get; set; }
        public int MessageCacheSize { get; set; }

        /// <summary>
        /// Custom configuration of the client 
        /// </summary>
        /// <param name="token">of the client to login to</param>
        /// <param name="shardIds">Ids of shards for the client</param>
        /// <param name="messageCacheSize">max size of the message cache</param>
        public ClientConfiguration(string token, int[] shardIds, int messageCacheSize)
        {
            Token = token;
            ShardIds = shardIds;
            MessageCacheSize = messageCacheSize;
        }

        /// <summary>
        /// Base configuration for a client
        /// </summary>
        public ClientConfiguration() : this("your_token_here", new[] {0}, 100)
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