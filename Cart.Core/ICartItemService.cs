namespace Cart.Core
{
    using System.Threading.Tasks;

    public interface ICartItemService
    {
        Task AddCartItemAsync(CartItem cartItem);
    }
}
