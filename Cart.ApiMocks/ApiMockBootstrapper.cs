namespace Cart.ApiMocks
{
    using Cart.Core.IoC;

    public static class ApiMockBootstrapper
    {
        // Binds all services to default implementations
        public static void BindDependencies()
            => AssemblyBinder.BindAssemby(typeof(ApiMockBootstrapper).Assembly);
    }
}
