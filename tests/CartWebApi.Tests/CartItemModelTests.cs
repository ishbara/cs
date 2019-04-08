namespace CartWebApi.Tests.Integration
{
    using System;
    using System.Threading.Tasks;
    using CartWebAPI.Model;
    using Xunit;

    public class CartItemModelTests
    {
        [Fact]
        public void Converts_To_CartItem()
        {
            Random rand = new Random();
            var request = new AddCartItemRequest
            {
                ProductId = rand.Next(),
                UserId = rand.Next(),
                Quantity = rand.Next()
            };

            var cartItem = request.ToCartItem();
            Assert.Equal(request.ProductId, cartItem.ProductId);
            Assert.Equal(request.UserId, cartItem.UserId);
            Assert.Equal(request.Quantity, cartItem.Quantity);
        }
    }
}
