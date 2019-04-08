namespace Cart.Core.Data
{
    using System.Threading.Tasks;

    public interface ICartItemDataGateway
    {
        Task AddCartItemAsync(CartItem cartItem);
    }
}
