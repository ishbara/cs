namespace Cart.Core
{
    public class CartItem
    {
        public CartItem(int productId, int userId, int quantity)
        {
            this.ProductId = productId;
            this.UserId = userId;
            this.Quantity = quantity;
        }

        public int ProductId { get; }

        public int UserId { get; }

        public int Quantity { get; }
    }
}
