namespace CartWebAPI.Model
{
    using Cart.Core;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddCartItemRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public CartItem ToCartItem()
            => new CartItem(this.ProductId, this.UserId, this.Quantity);
    }
}
