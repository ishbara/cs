namespace CartWebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }
    }
}