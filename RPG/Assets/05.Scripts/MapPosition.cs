using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosition : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = UIManager.instace.FindObjcet("PanelJoystick").GetComponent<PanelJoystick>().RetrunPosition();
    }
}
