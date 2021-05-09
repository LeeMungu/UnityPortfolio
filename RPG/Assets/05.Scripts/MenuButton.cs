using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public event System.Action<MenuInfo> MenuPlayDown;
    [SerializeField] MenuInfo m_menuInfo;

    private void Start()
    {
        
    }
    void OnButtonDown()
    {
        if (MenuPlayDown != null)
            MenuPlayDown(m_menuInfo);
    }
}
