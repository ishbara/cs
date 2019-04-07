namespace CartWebAPI.Model
{
    using System.ComponentModel.DataAnnotations;

    public class AddCartItemRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
