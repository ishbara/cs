namespace Cart.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// Exposes methods for cart item operations
    /// </summary>
    public interface ICartItemService
    {
        Task AddCartItemAsync(NewCartItem cartItem);
    }
}
