namespace CartWebApi.Tests
{
    using System.Threading.Tasks;
    using Cart.Core;
    using Cart.Core.Diagnostics;
    using CartWebAPI.Controllers;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class CartItemControllerTests
    {
        [Fact]
        public async Task Returns_BadRequest_When_Exc_Thrown_Async()
        {
            var serviceMock = new Mock<ICartItemService>();
            string message = "Test Exception";
            var exc = new CartException(CartErrorCode.InvalidProduct, message);
            serviceMock.Setup(s => s.AddCartItemAsync(It.IsAny<NewCartItem>())).Throws(exc);

            var controler = new CartItemController(serviceMock.Object);
            var result = await controler.PostAsync(new AddCartItemRequest());
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(message, ((BadRequestObjectResult)result).Value);
        }
    }
}
