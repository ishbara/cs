namespace Cart.Core.Core.IoC
{
    using System;

    /// <summary> Represents an IoC container. </summary>
    public interface IOCContainer
    {
        /// <summary> Gets an instance of T injected with all dependencies. </summary>
        /// <typeparam name="T">Type of the object that is requested.</typeparam>
        /// <returns>A new instance of requested type.</returns>
        T Get<T>()
            where T : class;

        /// <summary> Binds a certain service to its given implementation. </summary>
        /// <param name="service">Type of the service to be bound.</typeparam>
        /// <param name="implementation">Type of the implemenation of the service</typeparam>
        /// <param name="scope">The scope of the binding</param>
        void Bind(Type service, Type implementation, BindingScope scope);
    }
}