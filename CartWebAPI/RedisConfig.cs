namespace CartWebAPI
{
    using System.IO;
    using Cart.Redis;
    using Newtonsoft.Json;

    public class RedisConfig : IRedisConfiguration
    {
        private readonly RedisConfigModel model;

        private RedisConfig(RedisConfigModel model)
        {
            this.model = model;
        }

        public string RedisEndpoint => this.model.RedisEndpoint;

        public static IRedisConfiguration Read()
        {
            string json = File.ReadAllText("redisSettings.json");
            var configModel = JsonConvert.DeserializeObject<RedisConfigModel>(json);
            return new RedisConfig(configModel);
        }
    }
}
