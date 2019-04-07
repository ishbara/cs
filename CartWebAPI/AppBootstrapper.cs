namespace CartWebAPI
{
    using Cart.Core;
    using Cart.Core.IoC;
    using Cart.SimpleInjector;

    /// <summary>
    /// Performs application startup functions.
    /// </summary>
    public static class AppBootstrapper
    {
        /// <summary>
        /// Initializes DI container and binds all dependencies
        /// </summary>
        public static void InitializeDI()
        {
            SimpleInjectorContainer.Initialize();
            CoreBootstrapper.BindDependencies();

            AssemblyBinder.BindAssemby(typeof(AppBootstrapper).Assembly);
        }
    }
}
