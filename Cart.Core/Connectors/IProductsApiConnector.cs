namespace Cart.Core.Connectors
{
    public interface IProductsApiConnector
    {
        bool ProductExists(int productId);

        int GetStock(int productId);
    }
}