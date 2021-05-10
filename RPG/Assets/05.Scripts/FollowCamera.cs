using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;        //따라갈 타겟
    [SerializeField] float m_distance = 10f;    //타겟과의 거리 
    [SerializeField] float m_height = 5f;       //카메라 높이
    [SerializeField] float m_targetHeight = 2f; //따라갈 목표점 높이
    [SerializeField] float m_targetEulerAngleY = 0f;    //타겟 중심으로 회전할 회전값
    [SerializeField] float m_rotateSpeed = 5f;  //회전 스피드
    public float targetEnlerAngleY { get { return m_targetEulerAngleY; } set { m_targetEulerAngleY = value; } }

    public Vector3 offset = new Vector3(0f, 0f, 0f);

    private void Update()
    {
        m_target = GameManager.instace.FindObject("PlayerController").GetComponent<PlayerController>().target.transform;

        //카메라 회전
        //if (Input.GetKey("q"))
        //{
        //    // rotate toward left Yaxis
        //    transform.RotateAround(m_target.position, Vector3.up, 100.0f);
        //
        //    offset = transform.position - m_target.position;
        //    offset.Normalize();
        //}
        //if (Input.GetKey("e"))
        //{
        //    transform.RotateAround(m_target.position, Vector3.up, -100.0f);
        //
        //    offset = transform.position - m_target.position;
        //    offset.Normalize();
        //}

        //// 마우스 휠로 줌 인아웃
        //currentZoom -= Input.GetAxis("Mouse ScrollWheel");
        //// 줌 최소 및 최대 설정 
        //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //=================================================================================
        
    }
    



        //Update가 끝나고 실행되는게 LateUpdate
        private void LateUpdate()
    {
        //플레이어 따라가는 카메라
        //==============================================================
        //Quaternion.Euler : 오일러각도를 쿼터니언으로 변환해주는 함수
        Quaternion rotation = Quaternion.Euler(0f, m_targetEulerAngleY, 0f);

        //어떤 쿼터니언 값 * 방향벡터
        Vector3 toTarget = rotation * Vector3.forward * m_distance;
        //카메라의 높이 벡터
        Vector3 up = Vector3.up * m_height;
        //타겟의 높이 벡터
        //안높여 주면 그냥 땅을 바라보게 된다.
        Vector3 targetUp = Vector3.up * m_targetHeight;

        transform.position = (m_target.position + targetUp) - toTarget + up; //+ offset;
        //LookAt : 어떤 점을 forward축이 바라보게 해주는 함수
        transform.LookAt(m_target.position + targetUp);
        //=============================================================
    }

    private void OnDrawGizmos()
    {
        if (m_target == null)
            return;

        Vector3 targetUp = Vector3.up * m_targetHeight;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_target.position + targetUp);
    }
}