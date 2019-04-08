namespace Cart.ApiMocks
{
    using Cart.Core.Connectors;
    using Cart.Core.Core.IoC;

    /// <summary>
    /// A mock implementation for <see cref="IUserApiConnector"/>
    /// </summary>
    [BindOn(typeof(IUserApiConnector), BindingScope = BindingScope.Singleton)]
    public class UserApiMock : IUserApiConnector
    {
        /// <summary>
        /// This is just a mock implementation that returns true for values between 1 and 20
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>True if userId is between 1 and 20</returns>
        public bool UserExists(int userId)
        {
            return userId > 0 && userId <= 20;
        }
    }
}
