namespace Cart.Core
{
    using Cart.Core.Connectors;
    using System;

    public interface ICartItemService
    {
        void AddCartItem(CartItem cartItem);
    }

    public class CartItemService : ICartItemService
    {
        private readonly IProductsApiConnector productsApi;
        private readonly IUserApiConnector userApi;

        public CartItemService(
            IProductsApiConnector productsApi,
            IUserApiConnector userApi)
        {
            this.productsApi = productsApi;
            this.userApi = userApi;
        }

        public void AddCartItem(CartItem cartItem)
        {
            if (!this.productsApi.ProductExists(cartItem.ProductId))
            {
                throw new CartException(
                    CartItemErrorCode.InvalidProduct,
                    "Invalid product");
            }

            if (!this.userApi.UserExists(cartItem.UserId))
            {
                throw new CartException(
                    CartItemErrorCode.InvalidUser,
                    "Invalid user");
            }

            var stock = this.productsApi.GetStock(cartItem.ProductId);
            if (stock < cartItem.Quantity)
            {
                throw new CartException(
                    CartItemErrorCode.InsufficientStock,
                    "Insufficient stock");
            }

            throw new NotImplementedException();
        }
    }
}
