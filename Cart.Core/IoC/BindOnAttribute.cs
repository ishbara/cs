using System;

namespace Cart.Core.Core.IoC
{
    /// <summary> Specifies the type should be used for auto binding the specified interface. </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class BindOnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindOnAttribute"/>
        /// class with default scope of Transient. </summary>
        /// <param name="bindingType"> Type which this implementation will auto bind on. </param>
        public BindOnAttribute(Type bindingType)
        {
            // Default binding scope
            this.BindingScope = BindingScope.Transient;
            this.BindingType = bindingType;
        }

        /// <summary> Gets the type which this implementation will auto bind on. </summary>
        public Type BindingType { get; }

        /// <summary> Gets or sets the auto binding scope. (Transient, Singleton) </summary>
        public BindingScope BindingScope { get; set; }
    }
}