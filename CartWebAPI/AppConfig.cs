namespace CartWebAPI
{
    using Cart.Core.Core.IoC;
    using Cart.Redis;

    [BindOn(typeof(IRedisConfiguration), BindingScope = BindingScope.Singleton)]
    public class AppConfig : IRedisConfiguration
    {
        // TODO read from appsettings.json
        public string RedisEndpoint => "localhost";
    }
}
