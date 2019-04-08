namespace Cart.SimpleInjector
{
    using System;
    using Cart.Core.Core.IoC;
    using global::SimpleInjector;

    /// <summary> Implementation of IocContainer based on SimpleInjector </summary>
    public class SimpleInjectorContainer : IOCContainer
    {
        private SimpleInjectorContainer()
        {
            this.Container = new Container();
        }

        public Container Container { get; set; }

        public static SimpleInjectorContainer Initialize()
        {
            var containerInstance = new SimpleInjectorContainer();
            containerInstance.Container
                .RegisterInstance<IOCContainer>(containerInstance);
            CartContainer.Current = containerInstance;
            return containerInstance;
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

            this.Container.Register(service, implementation, lifeStyle);
        }

        public T Get<T>()
            where T : class
        {
            return this.Container.GetInstance<T>();
        }
    }
}