namespace Cart.Core.Tests
{
    using System.Threading.Tasks;
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
        public async Task Throws_Invalid_Product_Async()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.userApiMock
                .Setup(c => c.UserExists(It.IsAny<int>()))
                .Returns(false);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(false);
            var service = this.GetService();

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartItemErrorCode.InvalidProduct, exc.ErrorCode);
        }

        [Fact]
        public async Task Throws_InvalidUser_Async()
        {
            var cartItem = new CartItem(1, 2, 10);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(true);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(false);
            var service = this.GetService();

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartItemErrorCode.InvalidUser, exc.ErrorCode);
        }

        [Fact]
        public async Task Throws_InsufficientStock_Async()
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

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartItemErrorCode.InsufficientStock, exc.ErrorCode);
        }

        [Fact]
        public async Task Adds_CartItem_To_DataStore_Async()
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
                .Setup(d => d.AddCartItemAsync(cartItem))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = this.GetService();
            await service.AddCartItemAsync(cartItem);
            this.dataGatewayMock.Verify(g => g.AddCartItemAsync(cartItem));
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
