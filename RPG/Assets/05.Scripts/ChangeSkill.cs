using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkill : MonoBehaviour
{
    PlayerInfo m_playerInfo;
    [SerializeField] Sprite[] m_sprites = new Sprite[4];
    Image m_Image;
    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }
    private void Update()
    {
        if(m_playerInfo != GameManager.instace.TargetPlayerInfoOutput())
        {
            m_playerInfo = GameManager.instace.TargetPlayerInfoOutput();
            m_Image.sprite = m_sprites[m_playerInfo.Type - 1];
        }
    }
}
