namespace CartWebAPI.Controllers
{
    using System;
    using CartWebAPI.Model;
    using Microsoft.AspNetCore.Mvc;

    [Route("cart/item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IProductsApiConnector productsApi;

        public CartItemController(IProductsApiConnector productsApi)
        {
            this.productsApi = productsApi;
        }

        [HttpPost]
        public IActionResult Post(AddCartItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}