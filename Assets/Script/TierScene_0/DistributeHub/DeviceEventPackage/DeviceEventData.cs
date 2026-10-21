
using UnityEngine;


# region Screen Information -------------------------------
public sealed class ScreenAttribute
{
    private ScreenAttribute()
    {
        _width = Screen.width;
        _height = Screen.height;
    }
    static ScreenAttribute()
    {
        SingletonFactory<ScreenAttribute>.InitiateInstance(() => new ScreenAttribute());
    }
    readonly float _width;
    readonly float _height;

    public float ScreenWidth() { return _width; }
    public float ScreenHeight() { return _height; }
}
# endregion ----------------------------------------------------


# region Mouse Information -------------------------------------
[System.Flags]
public enum MouseStateCode
{
    COUNTMASK = (1 << 5) - 1, // 31
    IDLE = (1 << 6) - 1,
    OnIdle = 1 << 6,

    WAIT = (1 << 7) - 1,
    OnWait = 1 << 7,

    CLICKL = (1 << 8) - 1,
    OnClickL = 1 << 8,

    PRESSL = (1 << 9) - 1,
    OnPressL = 1 << 9,

    RELEASEL = (1 << 10) - 1,
    OnReleaseL = 1 << 10,

    CLICKR = (1 << 11) - 1,
    OnClickR = 1 << 11,

    PRESSR = (1 << 12) - 1,
    OnPressR = 1 << 12,

    RELEASER = (1 << 13) - 1,
    OnReleaseR = 1 << 13,

    DESTORY = 1 << 14,
    OnDestory = 1 << 14,

    CLEAN = (1 << 15) - 1,
    OnClean = 1 << 15
}

public sealed class MouseEvent : EventKit<MouseEvent, DeviceEvent>
{
    #region Singleton ------------------------------------
    private MouseEvent() : base()
    {
        eventStateCode = new MouseStateCode();
        eventStateCode = MouseStateCode.OnIdle;
    }
    static MouseEvent()
    {
        SingletonFactory<MouseEvent>.InitiateInstance(() => new MouseEvent());
    }
    #endregion --------------------------------------------
    #region Initiating Argument ---------------------------
    public MouseStateCode eventStateCode { get; private set; }
    public bool click_l { get; private set; } = false;
    public bool click_m { get; private set; } = false;
    public bool click_r { get; private set; } = false;
    public float time_press { get; private set; } = 0.0f;
    #endregion --------------------------------------------
    #region General Calling Functions ---------------------
    protected override void Distritbution()
    {
        Mouse0Event();
    }

    protected override void Recyle()
    {
        Debug.Log("当前鼠标状态: " + eventStateCode);
        // 当事件停止时,还需要清理一次,才应该完全卡住,所以需要一个开关进行判断
        MouseEventRecycle();
    }
    #endregion --------------------------------------------
    #region Independence Functions-------------------------
    private Vector2 position = Vector2.zero;
    public Vector2 Position()
    {
        return Input.mousePosition;
    }
    public Vector2 Position(float x_adjust, float y_adjust)
    {
        Vector2 temp_pos = Input.mousePosition;
        return position = new Vector2(temp_pos.x - x_adjust, temp_pos.y - y_adjust);
    }

    private readonly float time_max_press = 0.1f;
    private void Mouse0Event()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click_l = true;
            if (eventStateCode == MouseStateCode.OnIdle) { eventStateCode = MouseStateCode.OnClickL; }
        }
        else if (Input.GetMouseButton(0))
        {
            time_press += Time.deltaTime;
            if ((eventStateCode & MouseStateCode.OnPressL) == 0 && (eventStateCode != MouseStateCode.OnWait))
                if (time_press >= time_max_press)
                {
                    eventStateCode = MouseStateCode.OnPressL;
                    position = Input.mousePosition;
                }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if ((eventStateCode & MouseStateCode.OnWait) == 0)
                eventStateCode = MouseStateCode.OnReleaseL;
        }
    }
    public void MouseEventRecycle()
    {
        click_l = false;
        click_r = false;
        switch (eventStateCode)
        {
            case MouseStateCode.OnIdle:
                {
                    break;
                }
            case MouseStateCode.OnWait:
                {
                    break;
                }
            case MouseStateCode.OnClickL:
                {
                    eventStateCode = MouseStateCode.OnIdle;
                    time_press = 0.0f;

                    break;
                }
            case MouseStateCode.OnPressL:
                {
                    eventStateCode = MouseStateCode.OnIdle;
                    goto case MouseStateCode.OnClean;
                }
            case MouseStateCode.OnReleaseL:
                {
                    eventStateCode = MouseStateCode.OnIdle;
                    position = Vector2.zero;
                    time_press = 0.0f;
                    goto case MouseStateCode.OnClean;
                }
            case MouseStateCode.OnClean:
                {
                    break;
                }
        }
    }
    #endregion --------------------------------------------
}
# endregion ----------------------------------------------------




