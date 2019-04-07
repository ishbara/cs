namespace Cart.ApiMocks
{
    using Cart.Core.Connectors;

    /// <summary>
    /// A mock implementation for <see cref="IProductsApiConnector"/>
    /// </summary>
    public class ProductApiMock : IProductsApiConnector
    {
        /// <summary>
        /// This is just a mock implementation always returns 20 for stock.
        /// </summary>
        /// <param name="productId">Any value of integer is accepted</param>
        /// <returns>20</returns>
        public int GetStock(int productId)
        {
            return 20;
        }

        /// <summary>
        /// This is just a mock implementation that returns true for values between 1 and 100
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns>True if productId is between 1 and 100</returns>
        public bool ProductExists(int productId)
        {
            return productId > 0 && productId <= 100;
        }
    }
}
