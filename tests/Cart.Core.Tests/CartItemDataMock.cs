namespace Cart.Core.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cart.Core.Data;

    public class CartItemDataMock : ICartItemDataGateway
    {
        private readonly List<CartItem> cartItems;

        public CartItemDataMock()
        {
            this.cartItems = new List<CartItem>();
        }

        public Task AddCartItemAsync(CartItem cartItem)
        {
            this.cartItems.Add(cartItem);
            return Task.CompletedTask;
        }

        public bool Contains(CartItem cartItem)
        {
            return this.cartItems.Contains(cartItem);
        }
    }
}
