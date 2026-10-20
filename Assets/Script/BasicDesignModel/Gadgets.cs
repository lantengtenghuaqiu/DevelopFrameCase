using System;
using System.Collections.Generic;

public struct PluseLock
{
    private bool _valve;
    public bool Pluse()
    {
        if (_valve == true) { _valve = false; return true; }
        return false;
    }
    public void Retrigger()
    {
        _valve = true;
    }
}
public struct SwitchLock
{
    public SwitchLock(bool trigger) { _trigger = trigger; }
    bool _trigger;
    public bool TriggerState()
    {
        return _trigger;
    }
    public void TurnOff()
    {
        _trigger = false;
    }
    public void TurnOn()
    {
        _trigger = true;
    }
}

public sealed class LockKitCatalog : SingletonDoubleLock<LockKitCatalog>
{
    public static LockKitCatalog Instance()
    {
        if (SingletonDoubleLock<LockKitCatalog>._instance == null)
        {
            lock (SingletonDoubleLock<LockKitCatalog>._locker)
            {
                if (SingletonDoubleLock<LockKitCatalog>._instance == null)
                {
                    SingletonDoubleLock<LockKitCatalog>._instance = new LockKitCatalog();
                }
            }
        }
        return SingletonDoubleLock<LockKitCatalog>._instance;
    }
    private LockKitCatalog() { }
    // 全局锁应该由外部事件锁住而不是内部某一事件锁住        
    public static bool lockGlobal;
    private Dictionary<Type, bool> lockBook = new Dictionary<Type, bool>();
    public void Include(Type type)
    {
        bool get;
        if (!lockBook.TryGetValue(type, out get))
        {
            lockBook[type] = false;
        }
    }
    public bool Find(Type type)
    {
        bool get = false;
        lockBook.TryGetValue(type, out get);

        return get;
    }
    public void OpenShared(Type type)
    {
        bool get = false;
        if (lockBook.TryGetValue(type, out get))
            lockBook[type] = false;
        else
            throw new ArgumentException("锁缺失: " + type.ToString());
    }

    public void LockShared(Type type)
    {
        bool get = false;
        if (lockBook.TryGetValue(type, out get))
            lockBook[type] = true;
        else
            throw new ArgumentException("锁缺失: " + type.ToString());
    }



}
public sealed class LockKitRegister<T>
{
    private static Type type = typeof(T);
    private bool lockLocal;
    public LockKitRegister() { LockKitCatalog.Instance().Include(type); }
    public bool CheckLock()
    {
        if (lockLocal || LockKitCatalog.Instance().Find(type) || LockKitCatalog.lockGlobal) { return true; }
        return false;
    }

    // 钥匙部分:
    // 开锁
    public void OpenLocal()
    {
        lockLocal = false;
    }

    // 上锁
    public void LockLocal()
    {
        lockLocal = true;
    }

}