namespace Cart.Core.Core.IoC
{
    /// <summary> Scope for the auto binding of dependencies. </summary>
    public enum BindingScope
    {
        /// <summary> Type will be auto bind on Transient scope. A new instance will be created every time it is requested. </summary>
        Transient,

        /// <summary> Type will be auto bind on Singleton scope. Only one instance will exists in the application domain, and it will be returned  every time it is requested. The Singleton
        /// type must be thread-safe. </summary>
        Singleton
    }
}