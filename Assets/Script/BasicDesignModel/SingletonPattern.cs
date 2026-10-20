// public abstract class SingletonDoubleLock<T>
// {
//     protected static T _instance;
//     protected static readonly object _locker = new object();
//     // public static T Instance() { if (_instance == null) { lock (_locker) { if (_instance == null) { _instance = new T(); } } } return _instance; }
// }

public interface SingletonDoubleLock<T>
{
    protected static T _instance;
    protected static readonly object _locker = new object();
    // protected static T Instance();
    // public static T Instance() { if (_instance == null) { lock (_locker) { if (_instance == null) { _instance = new T(); } } } return _instance; }
}
