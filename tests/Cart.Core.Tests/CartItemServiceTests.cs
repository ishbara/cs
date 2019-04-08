namespace Cart.Core.Tests
{
    using System.Threading.Tasks;
    using Cart.Core.Connectors;
    using Cart.Core.Diagnostics;
    using Moq;
    using Xunit;

    public class CartItemServiceTests
    {
        private readonly Mock<IProductsApiConnector> productApiConnectorMock;
        private readonly Mock<IUserApiConnector> userApiMock;
        private readonly CartItemDataMock dataMock;

        public CartItemServiceTests()
        {
            this.productApiConnectorMock = new Mock<IProductsApiConnector>();
            this.userApiMock = new Mock<IUserApiConnector>();
            this.dataMock = new CartItemDataMock();
        }

        [Fact]
        public async Task Throws_Invalid_Product_Async()
        {
            var cartItem = new NewCartItem(1, 2, 10);
            this.MoqDefaultSetup(cartItem);
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(false);
            var service = this.GetService();

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartErrorCode.InvalidProduct, exc.ErrorCode);
        }

        [Fact]
        public async Task Throws_InvalidUser_Async()
        {
            var cartItem = new NewCartItem(1, 2, 10);
            this.MoqDefaultSetup(cartItem);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(false);
            var service = this.GetService();

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartErrorCode.InvalidUser, exc.ErrorCode);
        }

        [Fact]
        public async Task Throws_InsufficientStock_Async()
        {
            var cartItem = new NewCartItem(1, 2, 10);
            this.MoqDefaultSetup(cartItem);
            this.productApiConnectorMock
                .Setup(c => c.GetStock(cartItem.ProductId))
                .Returns(cartItem.Quantity - 1);
            var service = this.GetService();

            var exc = await Assert.ThrowsAsync<CartException>(
                () => service.AddCartItemAsync(cartItem));
            Assert.Equal(CartErrorCode.InsufficientStock, exc.ErrorCode);
        }

        [Fact]
        public async Task Adds_CartItem_To_DataStore_Async()
        {
            var cartItem = new NewCartItem(1, 2, 10);
            this.MoqDefaultSetup(cartItem);

            var service = this.GetService();
            await service.AddCartItemAsync(cartItem);
            var userCart = await this.dataMock.GetUserCartAsync(cartItem.UserId);
            Assert.Equal(userCart.UserId, cartItem.UserId);
            Assert.Equal(1, userCart.Items.Count);
            Assert.Equal(userCart.Items[cartItem.ProductId].Quantity, cartItem.Quantity);
        }

        [Fact]
        public async Task Increments_Quantity_When_Product_Exists()
        {
            var cartItem = new NewCartItem(1, 2, 2);
            var secondItem = new NewCartItem(cartItem.UserId, cartItem.ProductId, 3);
            int totalQuantity = cartItem.Quantity + secondItem.Quantity;
            this.MoqDefaultSetup(cartItem);
            this.productApiConnectorMock
                .Setup(c => c.GetStock(cartItem.ProductId))
                .Returns(totalQuantity + 1);

            var service = this.GetService();
            await service.AddCartItemAsync(cartItem);
            var userCart = await this.dataMock.GetUserCartAsync(cartItem.UserId);

            Assert.Equal(userCart.Items[cartItem.ProductId].Quantity, cartItem.Quantity);

            await service.AddCartItemAsync(secondItem);
            userCart = await this.dataMock.GetUserCartAsync(cartItem.UserId);
            Assert.Equal(userCart.Items[cartItem.ProductId].Quantity, totalQuantity);
        }

        private void MoqDefaultSetup(NewCartItem cartItem)
        {
            this.productApiConnectorMock
                .Setup(c => c.ProductExists(cartItem.ProductId))
                .Returns(true);
            this.productApiConnectorMock
                .Setup(c => c.GetStock(cartItem.ProductId))
                .Returns(cartItem.Quantity + 1);
            this.userApiMock
                .Setup(c => c.UserExists(cartItem.UserId))
                .Returns(true);
        }

        private CartItemService GetService()
        {
            return new CartItemService(
                this.productApiConnectorMock.Object,
                this.userApiMock.Object,
                this.dataMock);
        }
    }
}
