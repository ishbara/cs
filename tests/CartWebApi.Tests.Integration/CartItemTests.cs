namespace CartWebApi.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Cart.Redis;
    using CartWebAPI;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using Xunit;

    public sealed class CartItemTests
        : IClassFixture<WebApplicationFactory<Startup>>,
        IDisposable
    {
        private const string Url = "cart/item";
        private readonly WebApplicationFactory<Startup> factory;
        private readonly ConnectionMultiplexer redisMux;

        public CartItemTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            var redisOptions = new ConfigurationOptions
            {
                AllowAdmin = true,
                EndPoints =
                {
                    { "localhost", 6379 },
                },
            };

            this.redisMux = ConnectionMultiplexer.Connect(redisOptions);
        }

        [Fact]
        public async Task Returns_400_For_No_Input()
        {
            var client = this.GetClient();
            var result = await client.PostAsJsonAsync<AddCartItemRequest>(Url, null);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Connects_To_Redis()
        {
            var redisDb = this.InitializeRedis();
            string key = "test" + Guid.NewGuid();
            await redisDb.KeyDeleteAsync(key);
            await redisDb.StringSetAsync(key, "testData");
            var value = await redisDb.StringGetAsync(key);
            Assert.Equal("testData", value);
            await redisDb.KeyDeleteAsync(key);
        }

        [Fact]
        public async Task Can_Add_New_CartItem_Async()
        {
            var redisDb = this.InitializeRedis();
            var client = this.GetClient();
            var request = new AddCartItemRequest
            {
                ProductId = 46,
                UserId = 14,
                Quantity = 3
            };
            var userCartData = new UserCartData
            {
                UserId = request.UserId,
                CartItems = new Dictionary<int, int>
                {
                    { request.ProductId, request.Quantity }
                }
            };
            var serializedData = JsonConvert.SerializeObject(userCartData);
            string key = "cart:" + userCartData.UserId;
            await redisDb.KeyDeleteAsync(key);

            var response = await client.PostAsJsonAsync(Url, request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var storedValue = await redisDb.StringGetAsync(key);
            Assert.Equal(serializedData, storedValue.ToString(), StringComparer.InvariantCulture);
            await redisDb.KeyDeleteAsync(key);
        }

        public void Dispose()
        {
            this.redisMux.Dispose();
        }

        private HttpClient GetClient() =>
            this.factory.CreateDefaultClient();

        private IDatabase InitializeRedis()
        {
            this.redisMux.GetServer("localhost:6379");
            return this.redisMux.GetDatabase();
        }
    }
}
