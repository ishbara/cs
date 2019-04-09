﻿namespace Cart.Core
{
    using System.Threading.Tasks;
    using Cart.Core.Connectors;
    using Cart.Core.Core.IoC;
    using Cart.Core.Data;

    [BindOn(typeof(ICartItemService))]
    public class CartItemService : ICartItemService
    {
        private readonly IProductsApiConnector productsApi;
        private readonly IUserApiConnector userApi;
        private readonly ICartItemDataGateway dataGateway;

        public CartItemService(
            IProductsApiConnector productsApi,
            IUserApiConnector userApi,
            ICartItemDataGateway dataGateway)
        {
            this.productsApi = productsApi;
            this.userApi = userApi;
            this.dataGateway = dataGateway;
        }

        public Task AddCartItemAsync(CartItem cartItem)
        {
            this.ValidateForNewCartItem(cartItem);
            return this.dataGateway.AddCartItemAsync(cartItem);
        }

        private void ValidateForNewCartItem(CartItem cartItem)
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
        }
    }
}
