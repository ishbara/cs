namespace Cart.Core
{
    using Cart.Core.Connectors;
    using Cart.Core.Diagnostics;

    public class UserCartItem
    {
        public UserCartItem(int productId)
        {
            this.ProductId = productId;
        }

        public UserCartItem(int productId, int quantity)
            : this(productId)
        {
            this.Quantity = quantity;
        }

        public int ProductId { get; }

        public int Quantity { get; private set; }

        public void IncreaseQuantity(IProductsApiConnector productsApi, int quantity)
        {
            var stock = productsApi.GetStock(this.ProductId);
            if (stock < this.Quantity + quantity)
            {
                throw new CartException(
                    CartErrorCode.InsufficientStock,
                    "Insufficient stock");
            }

            this.Quantity += quantity;
        }
    }
}
