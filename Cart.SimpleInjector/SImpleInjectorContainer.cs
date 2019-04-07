namespace Cart.SimpleInjector
{
    using System;
    using Cart.Core.Core.IoC;
    using global::SimpleInjector;

    /// <summary> Implementation of IocContainer based on SimpleInjector </summary>
    public class SimpleInjectorContainer : IocContainer
    {
        private readonly Container container;

        private SimpleInjectorContainer(Container container)
        {
            this.container = container;
        }

        public static void Initialize()
        {
            var siContainer = new Container();
            siContainer.RegisterInstance<IocContainer>(new SimpleInjectorContainer(siContainer));
            CartContainer.Current = siContainer.GetInstance<IocContainer>();
        }

        public void Bind(Type service, Type implementation, BindingScope scope)
        {
            Lifestyle lifeStyle;
            switch (scope)
            {
                case BindingScope.Transient:
                    lifeStyle = Lifestyle.Transient;
                    break;
                case BindingScope.Singleton:
                    lifeStyle = Lifestyle.Singleton;
                    break;
                default:
                    throw new ArgumentException(
                        nameof(scope),
                        $"Binding scope {scope} is not handled");
            }

            this.container.Register(service, implementation, lifeStyle);
        }

        public T Get<T>()
            where T : class
        {
            return this.container.GetInstance<T>();
        }
    }
}