namespace Cart.Core.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cart.Core.Data;

    public class CartItemDataMock : ICartItemDataGateway
    {
        private readonly Dictionary<int, UserCart> userCarts;

        public CartItemDataMock()
        {
            this.userCarts = new Dictionary<int, UserCart>();
        }

        public Task<UserCart> GetUserCartAsync(int userId)
        {
            if (this.userCarts.TryGetValue(userId, out UserCart cart))
            {
                return Task.FromResult(cart);
            }

            return Task.FromResult<UserCart>(null);
        }

        public Task SaveUserCartAsync(UserCart userCart)
        {
            this.userCarts[userCart.UserId] = userCart;
            return Task.CompletedTask;
        }
    }
}
