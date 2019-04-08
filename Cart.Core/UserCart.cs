namespace Cart.Core
{
    using System;
    using System.Collections.Generic;
    using Cart.Core.Connectors;
    using Cart.Core.Diagnostics;

    /// <summary>
    /// Represents the cart of a specific user
    /// </summary>
    public class UserCart
    {
        private readonly Dictionary<int, UserCartItem> items;

        public UserCart(int userId)
        {
            this.UserId = userId;
            this.items = new Dictionary<int, UserCartItem>();
        }

        public UserCart(int userId, Dictionary<int, UserCartItem> cartItems)
        {
            this.UserId = userId;
            this.items = cartItems;
        }

        public int UserId { get; }

        public IReadOnlyDictionary<int, UserCartItem> Items => this.items;

        /// <summary>
        /// Adds new item to the cart. If the product exists increases its quantity
        /// </summary>
        /// <param name="productsApi">Products Api Gateway</param>
        /// <param name="newCartItem">NewCartItem to be added</param>
        internal void AddToCart(IProductsApiConnector productsApi, NewCartItem newCartItem)
        {
            if (productsApi == null)
            {
                throw new ArgumentNullException(nameof(productsApi));
            }

            if (newCartItem.UserId != this.UserId)
            {
                throw new ArgumentException(
                    "Supplied cartItem has a different user then this user cart",
                    nameof(newCartItem));
            }

            // If the product is already in the cart, there's no need to check for existence from api.
            bool newProduct = false;
            if (!this.items.TryGetValue(newCartItem.ProductId, out UserCartItem userCartItem))
            {
                userCartItem = InitializeUserCartItem(productsApi, newCartItem.ProductId);
                newProduct = true;
            }

            userCartItem.IncreaseQuantity(productsApi, newCartItem.Quantity);
            if (newProduct)
            {
                this.items.Add(userCartItem.ProductId, userCartItem);
            }
        }

        private static UserCartItem InitializeUserCartItem(
            IProductsApiConnector productsApi,
            int productId)
        {
            if (!productsApi.ProductExists(productId))
            {
                throw new CartException(
                    CartErrorCode.InvalidProduct,
                    "Invalid product");
            }

            return new UserCartItem(productId);
        }
    }
}
