namespace Cart.Core
{
    using System.Threading.Tasks;
    using Cart.Core.Connectors;
    using Cart.Core.Core.IoC;
    using Cart.Core.Data;
    using Cart.Core.Diagnostics;

    /// <summary>
    /// Implements Cart item operations
    /// </summary>
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

        /// <summary>
        /// Adds new cart item to the specified user.
        /// If the product is already in cart increases its quantity
        /// </summary>
        /// <param name="cartItem">Cart item to be added.</param>
        /// <returns>Awaitable task</returns>
        public async Task AddCartItemAsync(NewCartItem cartItem)
        {
            var userCart = await this.dataGateway.GetUserCartAsync(cartItem.UserId)
                .ConfigureAwait(false);
            if (userCart == null)
            {
                userCart = this.InitializeUserCart(cartItem);
            }

            // No need to check for existing user since the user cart is already created.
            userCart.AddToCart(this.productsApi, cartItem);
            await this.dataGateway.SaveUserCartAsync(userCart).ConfigureAwait(false);
        }

        private UserCart InitializeUserCart(NewCartItem cartItem)
        {
            if (!this.userApi.UserExists(cartItem.UserId))
            {
                throw new CartException(
                    CartErrorCode.InvalidUser,
                    "Invalid user");
            }

            return new UserCart(cartItem.UserId);
        }
    }
}
