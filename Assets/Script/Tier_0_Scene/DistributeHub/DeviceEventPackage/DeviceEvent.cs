using System;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(10)]
public class DeviceEvent : MonoBehaviour
{
    static Type type = typeof(DeviceEvent);
    [SerializeField] Button btn_Lock;
    [SerializeField] Button btn_Key;
    void Awake()
    {
        MouseEvent.Instance();
        ScreenAttribute.Instance();
        btn_Lock.onClick.AddListener(() => { LockKitCatalog.Instance().LockShared(type); });
        btn_Key.onClick.AddListener(() => { LockKitCatalog.Instance().OpenShared(type); });

    }
    void Update()
    {
        MouseEvent.Instance().EventRecyle();
        Debug.Log("Mouse Event Shared Lock: " + MouseEvent.Instance().lockKit.CheckLock());
        MouseEvent.Instance().EventDistritbution();

    }

    void FixedUpdate()
    {

    }
}
