using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSkillButton : MonoBehaviour
{
    public event System.Action<SkillInfo> EventPlaySkill;
    //컴퍼넌트를 다 가지고 있음
    SkillButton[] m_skillButtons;

    private void Start()
    {
        //모든 계층구조상의 자식에서 컴포넌트를 찾아온다.
        m_skillButtons = GetComponentsInChildren<SkillButton>();

        for(int i=0; i<m_skillButtons.Length; ++i)
        {
            m_skillButtons[i].EventPlaySkillDown += OnSkillButtonDown;
        }
    }
    //스킬버튼으로 부터 알림받으려는 함수
    void OnSkillButtonDown(SkillInfo skillInfo)
    {
        Debug.Log(skillInfo.StateNum);

        if (EventPlaySkill != null)
            EventPlaySkill(skillInfo);
    }
}
