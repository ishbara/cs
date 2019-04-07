namespace CartWebApi.Tests.Integration
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CartWebAPI;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc.Testing;
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

        private HttpClient GetClient() => 
            this.factory.CreateDefaultClient();
    }
}
