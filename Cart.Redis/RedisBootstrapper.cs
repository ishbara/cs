namespace Cart.Redis
{
    using Cart.Core.IoC;

    public static class RedisBootstrapper
    {
        // Binds all services to default implementations
        public static void BindDependencies()
            => AssemblyBinder.BindAssemby(typeof(RedisBootstrapper).Assembly);
    }
}
