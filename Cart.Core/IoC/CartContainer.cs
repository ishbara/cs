namespace Cart.Core.Core.IoC
{
    using System;

    public static class CartContainer
    {
        private static readonly object LockObject = new object();
        private static IOCContainer container;

        public static IOCContainer Current
        {
            get => container
                    ?? throw new InvalidOperationException("Service locator should be set " +
                    "in the main thread before accessing it.");

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // Service locator should only be set once
                // per application domain in the main thread
                // So locking should not cause performance problems
                lock (LockObject)
                {
                    if (container != null)
                    {
                        throw new InvalidOperationException(
                            "Service locator has already been set.");
                    }

                    container = value;
                }
            }
        }
    }
}
