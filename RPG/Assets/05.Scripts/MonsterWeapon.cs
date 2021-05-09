using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    int m_attackPoint = 10;
    //충돌시
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Player>().Damege(m_attackPoint);
        }
    }
}
