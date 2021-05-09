using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharButton : MonoBehaviour
{
    public event System.Action<PlayerInfo> EventPlayCharDown;

    [SerializeField] PlayerInfo m_playerInfo;
    public PlayerInfo playerInfo { set{ m_playerInfo = value; } get{ return m_playerInfo; } }
    Transform m_hpBar;
    Transform m_text;
    Transform m_icon;
    [SerializeField] Sprite[] m_icons = new Sprite[4];

    private void Awake()
    {
        GetComponent<CustomButton>().EventButtonDown += OnButtonDown;
        m_hpBar = transform.Find("HpBar");
        m_text = transform.Find("Text");
        m_icon = transform.Find("Icon");
    }
    private void Update()
    {
        if(m_playerInfo.Type>0&& m_playerInfo.Type<5)
        m_icon.GetComponent<Image>().sprite = m_icons[m_playerInfo.Type-1];
        m_text.GetComponent<Text>().text = m_playerInfo.Kname;
        m_hpBar.GetComponent<Image>().fillAmount = ((float)m_playerInfo.Hp) / ((float)m_playerInfo.MaxHp);
    }


    void OnButtonDown()
    {
        if (EventPlayCharDown != null)
            EventPlayCharDown(m_playerInfo);
    }
}
