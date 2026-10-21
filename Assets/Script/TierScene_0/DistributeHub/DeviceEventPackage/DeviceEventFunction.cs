
public partial class DeviceEvent
{

    public void InitButtons()
    {
        btn_Lock.onClick.AddListener(() =>
        {
            LockKitCatalog.LockShared(type);
        });
        btn_Key.onClick.AddListener(() => { LockKitCatalog.OpenShared(type); });
    }
}