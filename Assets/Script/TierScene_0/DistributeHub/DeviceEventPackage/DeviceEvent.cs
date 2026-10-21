using System;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(10)]
public partial class DeviceEvent : MonoBehaviour
{
    static Type type = typeof(DeviceEvent);
    [SerializeField] Button btn_Lock;
    [SerializeField] Button btn_Key;
    void Awake()
    {
        SingletonFactory<MouseEvent>.CreateInstance();
        SingletonFactory<ScreenAttribute>.CreateInstance();
        InitButtons();
    }
    void Update()
    {
        SingletonFactory<MouseEvent>.Instance().EventRecyle();
        SingletonFactory<MouseEvent>.Instance().EventDistritbution();
    }

}
