namespace CartWebAPI.Controllers
{
    using System;
    using Cart.Core;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemService cartItemService;

        public CartItemController(CartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpPost]
        public IActionResult Post(AddCartItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}