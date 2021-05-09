using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //GameObject[] 
    static UIManager s_instance = null;
    public static UIManager instace { get { return s_instance; } }
    private Dictionary<string, GameObject> m_UIList = new Dictionary<string, GameObject>();
    public GameObject m_panelJoystick { private set; get; }
    List<PlayerInfo> m_playerInfos;
    ChangeCharButton[] m_changeCharButtons = new ChangeCharButton[3];
    public void Awake()
    {
        s_instance = this;
        //씬이 바뀌어도 밑에 명령어쓰면 지워지지 않는다.
        //DontDestroyOnLoad(gameObject);

        AddList("PanelJoystick");
        AddList("PanelSkill");
        AddList("PanelChangeChar");
        AddList("PanelMenu1");
        AddList("PanelMenu2");
        AddList("CharPopup");
        AddList("UICamera");
        AddList("State");
    }
    private void Start()
    {
        m_changeCharButtons = FindObjcet("PanelChangeChar").GetComponentsInChildren<ChangeCharButton>();
        m_playerInfos = GameManager.instace.PlayerInfosOutput();
    }
    private void Update()
    {
        //좌상단 이미지의 경우
        int x = 0;
        for(int i=0; i<m_playerInfos.Count; ++i)
        {
            if(m_playerInfos[i].Name != 
                GameManager.instace.FindObject("PlayerController").GetComponent<PlayerController>().target.name)
            {
                m_changeCharButtons[x].playerInfo = m_playerInfos[i];
                x++;
            }
        }
        //하단 hp바
        int playerHp = GameManager.instace.FindObject("PlayerController").
            GetComponent<PlayerController>().target.GetComponent<Player>().playerInfo.Hp;
        int playerMaxHp = GameManager.instace.FindObject("PlayerController").
            GetComponent<PlayerController>().target.GetComponent<Player>().playerInfo.MaxHp;
        Transform hpBar = FindObjcet("State").transform.Find("HpBar").transform;
        hpBar.GetComponent<Image>().fillAmount = ((float)playerHp) / ((float)playerMaxHp);
        Transform hpText = FindObjcet("State").transform.Find("Text").transform;
        hpText.GetComponent<Text>().text = playerHp + "/" + playerMaxHp;
    }

    private void AddList(string temp)
    {
        m_UIList.Add(temp, GameObject.Find(temp));
    }

    public GameObject FindObjcet(string temp)
    {
        if (m_UIList[temp] == null)
            return null;

        return m_UIList[temp];
    }

    void SetPlayerInfo()
    {
        //m_playerInfos += { }
    }
}
