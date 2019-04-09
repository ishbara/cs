namespace CartWebAPI.Model
{
    using System.ComponentModel.DataAnnotations;
    using Cart.Core;

    /// <summary>
    /// Represents a new cart item request
    /// </summary>
    public class AddCartItemRequest
    {
        /// <summary>
        /// The id of the product to be added to cart. (Currently allowed 1 - 100)
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// The user id of the cart. (Currently allowed 1 - 20)
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The quantitiy of the product to be added to cart.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        public NewCartItem ToCartItem()
            => new NewCartItem(this.UserId, this.ProductId, this.Quantity);
    }
}
