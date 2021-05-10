using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    [SerializeField] float m_speed = 5f;
    float m_rotateSpeed = 5f;
    public GameObject target { get { return m_target; } }

    private void Awake()
    {
        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickMove += OnEventStickMove;
        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickUp += OnEventStickUp;
        GameObject.Find("PanelChangeChar").GetComponent<PanelChangeChar>().EventPlayPlayer += OnChangeTarget;
    }
    void OnEventStickMove(Vector3 dir)
    {
        //이동
        Vector3 worldDir = new Vector3();
        worldDir.x = dir.x * 10f;
        worldDir.y = 0f;
        worldDir.z = dir.y * 10f;

        Vector3 finalDir =
            //카메라기준으로 보는것
            Camera.main.transform.TransformDirection(worldDir) / 10;

        if (finalDir != Vector3.zero)
        {
            //회전
            //Quaternion targetRotation = Quaternion.LookRotation(finalDir.normalized);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_rotateSpeed * Time.deltaTime);
            //Debug.Log("targetRotation :     " + targetRotation);
            //Debug.Log("transform.rotation : " + transform.localRotation);

            finalDir.y = 0f;
            Move(finalDir);

            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0f;
            transform.LookAt(transform.position + direction * 2f);
            
            //플레이어 회전
            //Quaternion targetRotation = Quaternion.Euler(
            //new Vector3(0, Camera.main.transform.rotation.y,0).normalized);
            //transform.rotation = Quaternion.Euler( new Vector3(0, Camera.main.transform.rotation.y, 0));// Quaternion.Lerp(transform.rotation, targetRotation, m_rotateSpeed * Time.deltaTime);
            //Quaternion quat = transform.rotation;
            //quat.y = Camera.main.transform.rotation.y;
            //transform.rotation = quat;
            //Debug.Log(targetRotation);

            //Vector3 rotation = transform.eulerAngles;
            //rotation.y = Camera.main.transform.eulerAngles.y;
            //transform.eulerAngles = rotation;

            //애니메이션 - 각각 플레이어에서 처리하게 한다.
            //m_animator.SetFloat(m_animeHashKeyLeftRight, finalDir.x);
            //m_animator.SetFloat(m_animeHashKeyBackFront, finalDir.z);
        }
    }
    void OnEventStickUp()
    {
        //z축 초기화-후에 부드럽게 바꿔볼것
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);

        //각각 플레이어에서 처리하게 한다.
        //m_animator.SetFloat(m_animeHashKeyBackFront, 0f);
        //m_animator.SetFloat(m_animeHashKeyLeftRight, 0f);
    }


    void OnChangeTarget(PlayerInfo temp)
    {
        if(m_target == GameManager.instace.FindObject(temp.Name))
            return;
        m_target = GameManager.instace.FindObject(temp.Name);
        GameManager.instace.FindObject("HuTao").SetActive(false);
        GameManager.instace.FindObject("Jean").SetActive(false);
        GameManager.instace.FindObject("Noelle").SetActive(false);
        GameManager.instace.FindObject("Zhongli").SetActive(false);
        
        switch (temp.Name)
        {
            case "HuTao":
                GameManager.instace.FindObject("HuTao").SetActive(true);
                break;
            case "Jean":
                GameManager.instace.FindObject("Jean").SetActive(true);
                break;
            case "Noelle":
                GameManager.instace.FindObject("Noelle").SetActive(true);
                break;
            case "Zhongli":
                GameManager.instace.FindObject("Zhongli").SetActive(true);
                break;
        }
    }
    private void Move(Vector3 dir)
    {
        transform.Translate(dir * m_speed * Time.deltaTime, Space.World);
    }
}
