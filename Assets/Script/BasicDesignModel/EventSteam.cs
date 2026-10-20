
public abstract class EventKit<T, K> : SingletonDoubleLock<T>
{
    protected EventKit() { lockKit = new LockKitRegister<K>(); }
    public LockKitRegister<K> lockKit;
    public void EventDistritbution()
    {
        if (lockKit.CheckLock() == true) { return; }
        Distritbution();
    }

    public void EventRecyle()
    {
        bool lockstate = lockKit.CheckLock();
        if (switch_RecycleLock.TriggerState() == false)
        {
            if (lockstate == false)
                switch_RecycleLock.TurnOn();
            else
                return;
        }
        if (lockstate == true) { switch_RecycleLock.TurnOff(); }
        Recyle();
    }

    protected abstract void Distritbution();

    protected SwitchLock switch_RecycleLock = new SwitchLock(true);
    protected abstract void Recyle();

}

