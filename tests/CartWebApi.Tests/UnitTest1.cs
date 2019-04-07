namespace CartWebApi.Tests
{
    using CartWebAPI.Controllers;
    using Xunit;

    public class CartItemTests
    {
        private readonly CartItemController controller;

        public CartItemTests()
        {
            this.controller = new CartItemController();
        }

        [Fact]
        public void CartItem_Initializes()
        {
            //TODO sample test, remove it
            Assert.NotNull(this.controller);
        }
    }
}
