# SingletonManager
An easy-to-use implementation of the [Singleton pattern](https://en.wikipedia.org/wiki/Singleton_pattern). Available for download as a [NuGet package](https://www.nuget.org/packages/SingletonManager/1.0.0).

## Documentation
There are two classes contained in the package, which are both available in the `Singletons` namespace.

### `SingletonManager`
Static manager class for dealing with singletons.

| Method                  | Parameters | Returns | Usage                                                                                |
|-------------------------|------------|---------|--------------------------------------------------------------------------------------|
| `Register<T>`           | `T`        | `T`     | Register the given object as a singleton of its type                                 |
| `RegisterOrReplace<T>`  | `T`        | `T`     | Register the given object as a singleton of its type, replacing it if already exists |
| `CreateAndRegister<T>`  |            |         | Create a new instance of the given type and register it as a singleton               |
| `Get<T>`                |            | `T`     | Return the object that is the singleton of the current type                          |
| `TryGet<T>`             | `out T`    | `bool`  | Set the `out` parameter as the singleton, return `true` if it exists                 |
| `Clear`                 |            |         | Clear the manager                                                                    |
| `Unregister<T>`         |            |         | Unregister the type from the manager                                                 |
| `TryUnregister<T>`      |            |         | Unregister the type from the manager if it exists                                    |
| `HasBeenRegistered <T>` |            | `bool`  | Return `true` if the type has a registered singleton                                 |

### `Singleton<T>`
Abstract base class for easily creating a singleton class. Inherit from this, providing the derived type as the type parameter, and the class will be automatically registered as a singleton. Note that inheriting from this class is not required, it is just as a helper.
