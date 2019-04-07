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
        private readonly WebApplicationFactory<Startup> factory;

        public CartItemTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("cart/item")]
        public async Task Returns_400_For_No_Input(string url)
        {
            //var request = new AddCartItemRequest
            //{
            //    ProductId = 0
            //};

            var client = this.GetClient();
            var result = await client.PostAsJsonAsync(url, "");
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        private HttpClient GetClient() => 
            this.factory.CreateDefaultClient();
    }
}
