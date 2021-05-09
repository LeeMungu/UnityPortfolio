using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    int m_attackPoint = 50;
    //충돌시
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.layer==LayerMask.NameToLayer("Enermy"))
        {
            other.GetComponent<Monster>().Damege(m_attackPoint);
        }
    }
}
