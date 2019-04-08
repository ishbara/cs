namespace Cart.Core
{
    public class NewCartItem
    {
        public NewCartItem(int userId, int productId, int quantity)
        {
            this.UserId = userId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public int ProductId { get; }

        public int UserId { get; }

        public int Quantity { get; }
    }
}
