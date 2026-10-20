
using UnityEngine;
using UnityEngine.UI;

/*
    场景分为三类,Tier_1;Tier_2和Tier_Dynamic.分别进行不同的分工和合作.
        Tier_1负责Tier_2和Tier_Dynamic.
        Tier_2负责Tier_Dynamic;(持有游戏进行中的动态场景属性)
        Tier_Dynamic负责自己;
    对于场景的加载与写在需要注意,先卸载自己本身记录到全局表中的数据,然后再卸载自己本身.
*/
[DefaultExecutionOrder(11)]
public partial class StartMenuLoader : MonoBehaviour
{
    [SerializeField] GameObject StartMenuGroup;
    [SerializeField] Button btn_GameStart;
    [SerializeField] Button btn_GameSetting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SceneLoader.Instance();
    }
    void Start()
    {
        btn_GameStart.onClick.AddListener(() => { SceneLoader.Instance().LoadScene("Tier_1_Scene"); StartMenuGroup.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
