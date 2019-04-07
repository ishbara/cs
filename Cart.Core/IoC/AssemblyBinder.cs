namespace Cart.Core.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Cart.Core.Core.IoC;

    /// <summary> Provides methods for automatically binding dependent types. </summary>
    public static class AssemblyBinder
    {
        /// <summary> Auto binds all referenced types within given assembly. </summary>
        /// <param name="assembly"> The assembly to be auto bound. </param>
        /// <remarks> This method should only be called once for each assembly. </remarks>
        public static void BindAssemby(Assembly assembly)
        {
            foreach (Type implementationType in assembly.GetTypes())
            {
                if (!implementationType.IsClass)
                {
                    continue;
                }

                List<BindOnAttribute> bindingAttributes =
                    implementationType.GetCustomAttributes<BindOnAttribute>(false).ToList();

                if (!bindingAttributes.Any())
                {
                    continue;
                }

                foreach (BindOnAttribute bindingAttribute in bindingAttributes)
                {
                    CartContainer.Current.Bind(
                        bindingAttribute.BindingType,
                        implementationType,
                        bindingAttribute.BindingScope);
                }
            }
        }
    }
}