namespace Singletons
{
    /// <summary>
    /// A helper base class to inherit from which automatically registers the type as a singleton with the SingletonManager
    /// </summary>
    /// <typeparam name="T">The derived type</typeparam>
    public abstract class Singleton<T> where T : class
    {
        public Singleton() => SingletonManager.Register(this as T);
    }
}
