namespace Cart.Redis
{
    using System.Collections.Generic;
    using Cart.Core;

    public class UserCartData
    {
        public int UserId { get; set; }

#pragma warning disable S4004 // Collection properties should be readonly
        public Dictionary<int, int> CartItems { get; set; }
#pragma warning restore S4004 // Collection properties should be readonly

        public static UserCartData FromUserCart(UserCart cart)
        {
            var cartData = new UserCartData
            {
                UserId = cart.UserId
            };

            foreach (var item in cart.Items)
            {
                cartData.CartItems.Add(item.Key, item.Value.Quantity);
            }

            return cartData;
        }

        public UserCart ToUserCart()
        {
            var cartItems = new Dictionary<int, UserCartItem>();
            foreach (var cartItem in this.CartItems)
            {
                cartItems.Add(cartItem.Key, new UserCartItem(cartItem.Key, cartItem.Value));
            }

            return new UserCart(this.UserId, cartItems);
        }
    }
}
