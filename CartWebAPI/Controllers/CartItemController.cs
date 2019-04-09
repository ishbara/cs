namespace CartWebAPI.Controllers
{
    using System.Threading.Tasks;
    using Cart.Core;
    using Cart.Core.Core.IoC;
    using Cart.Core.Diagnostics;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    [BindOn(typeof(CartItemController))]
    [Produces("text/plain", "application/json")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        /// <summary>
        /// Adds new item to a user's cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     {
        ///         "ProductId": 46,
        ///         "UserId": 14,
        ///         "Quantity": 2
        ///     }
        /// </remarks>
        /// <param name="request">New Cart Item request</param>
        /// <returns>Status of the operation</returns>
        /// <response code="200">Item successfully added</response>
        /// <response code="400">Cart item canno be added. Review your request.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

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