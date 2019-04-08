namespace CartWebAPI
{
    using Cart.ApiMocks;
    using Cart.Core;
    using Cart.Core.IoC;
    using Cart.Redis;
    using Cart.SimpleInjector;
    using SimpleInjector;

    /// <summary>
    /// Performs application startup functions.
    /// </summary>
    public static class AppBootstrapper
    {
        /// <summary>
        /// Initializes DI container and binds all dependencies
        /// </summary>
        /// <returns>The DI container instance</returns>
        public static Container InitializeDI()
        {
            var container = SimpleInjectorContainer.Initialize();
            CoreBootstrapper.BindDependencies();
            RedisBootstrapper.BindDependencies();
            ApiMockBootstrapper.BindDependencies();

            AssemblyBinder.BindAssemby(typeof(AppBootstrapper).Assembly);
            return container.Container;
        }
    }
}
