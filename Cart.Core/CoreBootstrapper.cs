namespace Cart.Core
{
    using Cart.Core.IoC;

    public static class CoreBootstrapper
    {
        // Binds all services to default implementations
        public static void BindDependencies()
            => AssemblyBinder.BindAssemby(typeof(CoreBootstrapper).Assembly);
    }
}
