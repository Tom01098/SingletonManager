using System;

namespace Singletons
{
    /// <summary>
    /// A helper base class to inherit from which automatically registers the type as a singleton with the SingletonManager
    /// </summary>
    /// <typeparam name="T">The derived type</typeparam>
    public abstract class Singleton<T> where T : class
    {
        public Singleton()
        {
            if (typeof(T) != GetType())
            {
                throw new InvalidOperationException("The type parameter for a Singleton must be the same as the derived type.");
            }

            if (SingletonManager.HasBeenRegistered<T>())
            {
                throw new InvalidOperationException(typeof(T).FullName + " has already been registered.");
            }

            SingletonManager.Register(this as T);
        }
    }
}
