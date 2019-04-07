namespace CartWebApi.Tests
{
    using Cart.Core;
    using CartWebAPI.Controllers;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class CartItemControllerTests
    {
        [Fact]
        public void Returns_BadRequest_When_Exc_Thrown()
        {
            var serviceMock = new Mock<ICartItemService>();
            string message = "Test Exception";
            var exc = new CartException(CartItemErrorCode.InvalidProduct, message);
            serviceMock.Setup(s => s.AddCartItem(It.IsAny<CartItem>())).Throws(exc);

            var controler = new CartItemController(serviceMock.Object);
            var result = controler.Post(new AddCartItemRequest());
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(message, ((BadRequestObjectResult)result).Value);
        }
    }
}
