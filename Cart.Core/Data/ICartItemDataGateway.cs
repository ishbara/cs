namespace Cart.Core.Data
{
    using System.Threading.Tasks;

    public interface ICartItemDataGateway
    {
        Task<UserCart> GetUserCartAsync(int userId);

        Task SaveUserCartAsync(UserCart userCart);
    }
}
