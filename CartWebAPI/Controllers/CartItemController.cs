namespace CartWebAPI.Controllers
{
    using System.Threading.Tasks;
    using Cart.Core;
    using Cart.Core.Core.IoC;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    [BindOn(typeof(CartItemController))]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(AddCartItemRequest request)
        {
            try
            {
                await this.cartItemService.AddCartItemAsync(request.ToCartItem());
                return this.Ok();
            }
            catch (CartException exc)
            {
                return this.BadRequest(exc.Message);
            }
        }
    }
}