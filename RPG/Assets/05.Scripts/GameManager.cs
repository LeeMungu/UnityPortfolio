using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ////들고 있을 오브젝트들
    //GameObject m_joystic

    static GameManager s_instance = null;
    public static GameManager instace { get { return s_instance; } }

    private Dictionary<string, GameObject> m_objectList =new Dictionary<string, GameObject>();
    bool m_isGameSeting = true;

    List<PlayerInfo> m_playerInfos = new List<PlayerInfo>();

    public void Awake()
    {
        s_instance = this;
        //씬이 바뀌어도 밑에 명령어쓰면 지워지지 않는다.
        //DontDestroyOnLoad(gameObject);
        AddList("PlayerController");
        AddList("HuTao");
        AddList("Jean");
        AddList("Noelle");
        AddList("Zhongli");
        m_playerInfos.Clear();
        m_playerInfos.Add(PlayerSeting("HuTao"));
        m_playerInfos.Add(PlayerSeting("Jean"));
        m_playerInfos.Add(PlayerSeting("Noelle"));
        m_playerInfos.Add(PlayerSeting("Zhongli"));
    }
    private void Start()
    {
        //캐릭터에 플레이어 info 넣어주기
        FindObject("HuTao").GetComponent<Player>().playerInfo = PlayerInfoOutput("HuTao");
        FindObject("Jean").GetComponent<Player>().playerInfo = PlayerInfoOutput("Jean");
        FindObject("Zhongli").GetComponent<Player>().playerInfo = PlayerInfoOutput("Zhongli");
        FindObject("Noelle").GetComponent<Player>().playerInfo = PlayerInfoOutput("Noelle");
    }
    private void Update()
    {
        if(m_isGameSeting)
        {
            //플레이어 셋팅
            m_isGameSeting = false;
            FindObject("HuTao").SetActive(false);
            FindObject("Jean").SetActive(false);
            FindObject("Zhongli").SetActive(false);

            FindObject("HuTao").GetComponent<Rigidbody>().isKinematic = false;
            FindObject("Jean").GetComponent<Rigidbody>().isKinematic = false;
            FindObject("Zhongli").GetComponent<Rigidbody>().isKinematic = false;
            FindObject("Noelle").GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void AddList(string temp)
    {
        m_objectList.Add(temp, GameObject.Find(temp));
    }

    public GameObject FindObject(string temp)
    {
        if (m_objectList[temp] == null)
            return null;

        return m_objectList[temp];
    }
    private PlayerInfo PlayerSeting(string name)
    {
        PlayerInfo result = new PlayerInfo();
        result.Name = name;

        if (result.Name == "HuTao" )
        {
            result.Kname = "호두";
            result.Type = 4;
            result.StateNum = 4;
            result.MaxHp = 100;
            result.MaxSp = 50;
            result.Hp = 100;
            result.Sp = 0;
        }
        else if(result.Name == "Jean")
        {
            result.Kname = "진";
            result.Type = 2;
            result.StateNum = 2;
            result.MaxHp = 100;
            result.MaxSp = 50;
            result.Hp = 100;
            result.Sp = 0;
        }
        else if(result.Name == "Noelle")
        {
            result.Kname = "노엘";
            result.Type = 1;
            result.StateNum = 1;
            result.MaxHp = 100;
            result.MaxSp = 50;
            result.Hp = 100;
            result.Sp = 0;
        }
        else if(result.Name == "Zhongli")
        {
            result.Kname = "종려";
            result.Type = 3;
            result.StateNum = 3;
            result.MaxHp = 100;
            result.MaxSp = 50;
            result.Hp = 100;
            result.Sp = 0;
        }

        return result;
    }

    public PlayerInfo PlayerInfoOutput(string name)
    {
        for(int i=0; i<m_playerInfos.Count; ++i)
        {
            if (m_playerInfos[i].Name == name)
                return m_playerInfos[i];
        }
        return null;
    }
    public List<PlayerInfo> PlayerInfosOutput()
    {
        return m_playerInfos;
    }
    public PlayerInfo TargetPlayerInfoOutput()
    {
        return FindObject("PlayerController").GetComponent<PlayerController>().
            target.GetComponent<Player>().playerInfo;
    }
}
