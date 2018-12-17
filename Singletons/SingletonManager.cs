using System;
using System.Collections.Generic;

namespace Singletons
{
    /// <summary>
    /// Manages singleton instances
    /// </summary>
    public static class SingletonManager
    {
        private static readonly Dictionary<Type, object> singletons;

        static SingletonManager()
        {
            singletons = new Dictionary<Type, object>();
        }

        #region Registering
        /// <summary>
        /// Register an object as the singleton of that type
        /// </summary>
        /// <typeparam name="T">The type that this is the singleton of</typeparam>
        /// <param name="obj">The object that is the singleton</param>
        /// <returns>The object set as the singleton</returns>
        public static T Register<T>(T obj)
        {
            var type = typeof(T);

            if (singletons.ContainsKey(type))
            {
                throw new InvalidOperationException(type.FullName + " has already been registered, do you mean to use " + nameof(RegisterOrReplace) + "?");
            }

            singletons.Add(type, obj);

            return obj;
        }

        /// <summary>
        /// Register an object as the singleton of that type, replacing it if it has already been registered
        /// </summary>
        /// <typeparam name="T">The type that this is the singleton of</typeparam>
        /// <param name="obj">The object that is the singleton</param>
        /// <returns>The object set as the singleton</returns>
        public static T RegisterOrReplace<T>(T obj)
        {
            var type = typeof(T);

            if (singletons.ContainsKey(type))
            {
                singletons[type] = obj;
            }
            else
            {
                singletons.Add(type, obj);
            }

            return obj;
        }

        /// <summary>
        /// Register a singleton with a type, using the parameterless constructor
        /// </summary>
        /// <typeparam name="T">The type to register a new object of</typeparam>
        /// <returns>The object set as the singleton</returns>
        public static T CreateAndRegister<T>() where T : new()
        {
            return Register(new T());
        }
        #endregion

        #region Getting
        /// <summary>
        /// Get the singleton object of a type
        /// </summary>
        /// <typeparam name="T">The type to get the singleton object for</typeparam>
        /// <returns>The object set as the singleton</returns>
        public static T Get<T>()
        {
            var type = typeof(T);

            if (!singletons.ContainsKey(type))
            {
                throw new InvalidOperationException(type.FullName + " has not been registed, do you mean to use " + nameof(TryGet) + "?");
            }

            return (T)singletons[type];
        }

        /// <summary>
        /// Attempt to get the singleton object of a type if it exists
        /// </summary>
        /// <typeparam name="T">The type to get the singleton object for</typeparam>
        /// <param name="obj">The result of getting the singleton if true is returned</param>
        /// <returns>True if the singleton exists</returns>
        public static bool TryGet<T>(out T obj)
        {
            bool success = singletons.TryGetValue(typeof(T), out var result);

            obj = (T)result;
            return success;
        }
        #endregion

        #region Utility
        /// <summary>
        /// Clear the manager
        /// </summary>
        public static void Clear()
        {
            singletons.Clear();
        }

        /// <summary>
        /// Unregister a singleton
        /// </summary>
        /// <typeparam name="T">The type to remove the singleton for</typeparam>
        public static void Unregister<T>()
        {
            var type = typeof(T);

            if (!singletons.ContainsKey(type))
            {
                throw new InvalidOperationException(type.FullName + " has not been registed, do you mean to use " + nameof(TryUnregister) + "?");
            }

            singletons.Remove(type);
        }

        /// <summary>
        /// Attempt to unregister a singleton if it exists
        /// </summary>
        /// <typeparam name="T">The type to remove the singleton for</typeparam>
        public static void TryUnregister<T>()
        {
            var type = typeof(T);

            if (singletons.ContainsKey(type))
            {
                singletons.Remove(type);
            }
        }

        /// <summary>
        /// Has the singleton been registered?
        /// </summary>
        /// <typeparam name="T">The type to check if it is registered</typeparam>
        /// <returns>True if it has been registered</returns>
        public static bool HasBeenRegistered<T>()
        {
            return singletons.ContainsKey(typeof(T));
        }
        #endregion
    }
}
