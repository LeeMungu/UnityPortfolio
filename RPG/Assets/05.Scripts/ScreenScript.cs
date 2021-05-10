using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //카메라 회전관련 v3
    float FirstPoint, SecondPoint;
    bool m_weelMode = false;
    Vector2 result;
    Camera m_uiCamera;
    
    private void Start()
    {
        m_uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }
    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        FirstPoint = Input.GetTouch(0).position;
        //    }
        //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {
        //        SecondPoint = Input.GetTouch(0).position;
        //        Camera.main.GetComponent<FollowCamera>().targetEnlerAngleY
        //            += (SecondPoint.x - FirstPoint.x) * 90 / Screen.width;
        //    }
        //
        //}

        if (m_weelMode)
        {
            Camera.main.GetComponent<FollowCamera>().targetEnlerAngleY
            = FirstPoint + (Input.mousePosition.x - SecondPoint) * 180f / Screen.width;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //RectTransformUtility!! 이 안에 CameraCanvas관련 함수들이 제공 
        
        //1.변환할 좌표 결과를 사용할 오브젝트의 부모 Transform
        //2.스크린 좌표
        //3. UICamera 
    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            m_uiCamera,
            out result
            );
        SecondPoint = Input.mousePosition.x;
    
        m_weelMode = true;
        FirstPoint = Camera.main.GetComponent<FollowCamera>().targetEnlerAngleY;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        //result = null;
        m_weelMode = false;
    
    }
}
