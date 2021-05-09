using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChangeChar : MonoBehaviour
{
    public event System.Action<PlayerInfo> EventPlayPlayer;
    ChangeCharButton[] m_changeCharButtons;
    private void Start()
    {
        m_changeCharButtons = GetComponentsInChildren<ChangeCharButton>();
        for(int i=0; i<m_changeCharButtons.Length; ++i)
        {
            m_changeCharButtons[i].EventPlayCharDown += OnButtonDown;
        }
    }
    void OnButtonDown(PlayerInfo playerInfo)
    {
        if (EventPlayPlayer != null)
            EventPlayPlayer(playerInfo);
    }
}
