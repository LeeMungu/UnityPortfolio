using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CustomButton->SkillButton->PanelSkillButton->인겜(플레이어)
public class SkillButton : MonoBehaviour
{
    public event System.Action<SkillInfo> EventPlaySkillDown;

    [SerializeField] SkillInfo m_skillInfo;

    Image m_imageSkillIcon;
    Image m_imageSkillCoolTime;

    Coroutine m_coroutineSkillCoolDown;

    private void Start()
    {
        m_imageSkillIcon = transform.Find("SkillButtonIcon").GetComponent<Image>();
        m_imageSkillCoolTime = transform.Find("CoolDown").GetComponent<Image>();
        m_imageSkillCoolTime.enabled = false;

        CustomButton button = transform.Find("SkillButtonIcon").GetComponent<CustomButton>();
        button.EventButtonDown += OnButtonDown;
    }

    void OnButtonDown()
    {
        if (m_coroutineSkillCoolDown == null)
            m_coroutineSkillCoolDown = StartCoroutine(CoroutineSkillCoolDown());
        if (EventPlaySkillDown != null)
            EventPlaySkillDown(m_skillInfo);
    }
    IEnumerator CoroutineSkillCoolDown()
    {
        //쿨타임 첫표시
        m_imageSkillCoolTime.enabled = true;
        m_imageSkillCoolTime.fillAmount = 1f;
        m_imageSkillIcon.color = new Color(m_imageSkillIcon.color.r, m_imageSkillIcon.color.r, m_imageSkillIcon.color.r, 0.2f );
        float currentTime = 0f;

        while (true)
        {
            currentTime += Time.deltaTime;
            float ratio = 1f - (currentTime / m_skillInfo.CoolTime);

            m_imageSkillCoolTime.fillAmount = ratio;

            if (currentTime >= m_skillInfo.CoolTime)
            {
                m_imageSkillIcon.color = new Color(m_imageSkillIcon.color.r, m_imageSkillIcon.color.r, m_imageSkillIcon.color.r, 1);
                m_imageSkillCoolTime.enabled = false;
                m_coroutineSkillCoolDown = null;
                break;
            }
            yield return null;
        }
    }
}