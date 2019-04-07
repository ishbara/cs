namespace Cart.Core.Tests
{
    using Cart.Core.Connectors;
    using Cart.Core.Data;
    using Moq;
    using Xunit;

    public class CartItemServiceTests
    {
        private readonly Mock<IProductsApiConnector> productApiConnectorMock;
        private readonly Mock<IUserApiConnector> userApiMock;
        private readonly Mock<ICartItemDataGateway> dataGatewayMock;

        public CartItemServiceTests()
        {
            this.productApiConnectorMock = new Mock<IProductsApiConnector>();
            this.userApiMock = new Mock<IUserApiConnector>();
            this.dataGatewayMock = new Mock<ICartItemDataGateway>();
        }

        [Fact]
        public void Throws_Invalid_Product()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.userApiMock
                .Setup(c => c.UserExists(It.IsAny<int>()))
                .Returns(false);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(false);
            var service = this.GetService();

            var exc = Assert.Throws<CartException>(
                () => service.AddCartItem(cartItem));
            Assert.Equal(CartItemErrorCode.InvalidProduct, exc.ErrorCode);
        }

        [Fact]
        public void Throws_InvalidUser()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(true);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(false);
            var service = this.GetService();

            var exc = Assert.Throws<CartException>(
                () => service.AddCartItem(cartItem));
            Assert.Equal(CartItemErrorCode.InvalidUser, exc.ErrorCode);
        }

        [Fact]
        public void Throws_InsufficientStock()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(true);
            this.productApiConnectorMock
                .Setup(c => c.GetStock(cartItem.ProductId))
                .Returns(cartItem.Quantity - 1);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(true);
            var service = this.GetService();

            var exc = Assert.Throws<CartException>(
                () => service.AddCartItem(cartItem));
            Assert.Equal(CartItemErrorCode.InsufficientStock, exc.ErrorCode);
        }

        [Fact]
        public void Adds_CartItem_To_DataStore()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(true);
            this.productApiConnectorMock
                .Setup(c => c.GetStock(cartItem.ProductId))
                .Returns(cartItem.Quantity + 1);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(true);
            this.dataGatewayMock
                .Setup(d => d.AddCartItem(cartItem))
                .Verifiable();

            var service = this.GetService();
            service.AddCartItem(cartItem);
            this.dataGatewayMock.Verify(g => g.AddCartItem(cartItem));
        }

        private CartItemService GetService()
        {
            return new CartItemService(
                this.productApiConnectorMock.Object,
                this.userApiMock.Object,
                this.dataGatewayMock.Object);
        }
    }
}
