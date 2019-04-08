namespace Cart.Redis
{
    using System.Threading.Tasks;
    using Cart.Core;
    using Cart.Core.Core.IoC;
    using Cart.Core.Data;
    using Newtonsoft.Json;
    using StackExchange.Redis;

    /// <summary>
    /// Implements a cart item data gateway to redis.
    /// </summary>
    [BindOn(typeof(ICartItemDataGateway), BindingScope = BindingScope.Singleton)]
    public class CartItemRedisGateway : ICartItemDataGateway
    {
        private readonly ConnectionMultiplexer mux;

        public CartItemRedisGateway(IRedisConfiguration redisConfig)
        {
            this.mux = ConnectionMultiplexer.Connect(redisConfig.RedisEndpoint);
        }

        public Task AddCartItemAsync(CartItem cartItem)
        {
            var db = this.mux.GetDatabase();
            string key = "cart:" + cartItem.UserId;
            return db.ListRightPushAsync(key, JsonConvert.SerializeObject(cartItem));
        }
    }
}
