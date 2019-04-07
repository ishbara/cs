namespace Cart.Core.Connectors
{
    public interface IUserApiConnector
    {
        bool UserExists(int userId);
    }
}