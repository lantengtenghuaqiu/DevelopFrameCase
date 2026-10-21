

public class DoubleCheck<T>
{
    protected static T _instance;
    protected static readonly object _locker = new object();
}

public sealed class SingletonFactory<T> : DoubleCheck<T>
{
    static System.Func<T> InstanceFactory;
    public static void InitiateInstance(System.Func<T> instance_factory)
    {
        if (instance_factory == null)
            throw new System.InvalidOperationException("工厂函数初始化函数为空: " + typeof(T).ToString());
        else
            InstanceFactory = instance_factory;
    }
    public static void CreateInstance()
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    // 关键：先让 T 的静态构造执行，工厂才会被注册
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
                    if (InstanceFactory == null)
                        throw new System.InvalidOperationException("工厂函数未初始化: " + typeof(T).ToString());
                    _instance = InstanceFactory();
                }
            }
        }
        else
            throw new System.InvalidOperationException("重复创建单例");
    }
    public static T Instance()
    {
        if (_instance == null)
            throw new System.ArgumentNullException("实例缺失" + typeof(T).ToString());
        else
            return _instance;
    }
}
