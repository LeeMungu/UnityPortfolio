using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextSkript : MonoBehaviour
{
    int m_textSize = 50;
    int m_maxTextSize = 120;
    float m_textPositionY = 0;
    Coroutine TextSizeCo;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(TextSizeCo ==null)
            TextSizeCo=StartCoroutine("TextSizeUp");
        
        
    }
    IEnumerator TextSizeUp()
    {
        while(m_textSize<m_maxTextSize)
        {
            m_textSize+=3;
            GetComponent<Text>().fontSize = m_textSize;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
            transform.position = pos;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(0.5f);
        //Destroy(gameObject);
    }
    public void DamageSet(int Damage, bool isEnermy)
    {
        GetComponent<Text>().text = Damage.ToString();
        if (isEnermy)
            GetComponent<Text>().color = new Color(255,122,0);
        else
            GetComponent<Text>().color = Color.white;
    }
}
