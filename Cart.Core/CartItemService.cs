namespace Cart.Core
{
    using System;

    public interface ICartItemService
    {
        void AddCartItem(CartItem cartItem);
    }

    public class CartItemService : ICartItemService
    {
        public void AddCartItem(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
