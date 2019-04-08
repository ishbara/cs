namespace CartWebApi.Tests.Integration
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CartWebAPI;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc.Testing;
    using StackExchange.Redis;
    using Xunit;

    public class CartItemTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string Url = "cart/item";
        private readonly WebApplicationFactory<Startup> factory;

        public CartItemTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
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
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                var redisDb = redis.GetDatabase();
                await redisDb.StringSetAsync("test", "testData");
                var value = await redisDb.StringGetAsync("test");
                Assert.Equal("testData", value);
            }
        }

        private HttpClient GetClient() =>
            this.factory.CreateDefaultClient();
    }
}
