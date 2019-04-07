namespace CartWebAPI.Controllers
{
    using Cart.Core;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpPost]
        public IActionResult Post(AddCartItemRequest request)
        {
            try
            {
                this.cartItemService.AddCartItem(request.ToCartItem());
                return this.Ok();
            }
            catch (CartException exc)
            {
                return this.BadRequest(exc.Message);
            }
        }
    }
}