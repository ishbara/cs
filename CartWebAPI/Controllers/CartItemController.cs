namespace CartWebAPI.Controllers
{
    using System;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        public CartItemController()
        {
        }

        [HttpPost]
        public IActionResult Post(AddCartItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}