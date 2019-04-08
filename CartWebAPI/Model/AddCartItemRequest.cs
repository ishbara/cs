namespace CartWebAPI.Model
{
    using System.ComponentModel.DataAnnotations;
    using Cart.Core;

    public class AddCartItemRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public NewCartItem ToCartItem()
            => new NewCartItem(this.UserId, this.ProductId, this.Quantity);
    }
}
