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

        public async Task<UserCart> GetUserCartAsync(int userId)
        {
            var db = this.mux.GetDatabase();
            var key = GetKey(userId);
            var rawResult = await db.StringGetAsync(key).ConfigureAwait(false);
            var userCartData = JsonConvert.DeserializeObject<UserCartData>(rawResult);
            return userCartData.ToUserCart();
        }

        public Task SaveUserCartAsync(UserCart userCart)
        {
            var db = this.mux.GetDatabase();
            var userCartData = UserCartData.FromUserCart(userCart);
            var key = GetKey(userCartData.UserId);
            return db.StringSetAsync(key, JsonConvert.SerializeObject(userCartData));
        }

        private static string GetKey(int userId)
        {
            return "cart:" + userId;
        }
    }
}
