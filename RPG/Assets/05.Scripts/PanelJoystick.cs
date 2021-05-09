using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelJoystick : MonoBehaviour, IPointerDownHandler,
    IDragHandler, IPointerUpHandler
{
    //event : delegate앞에 붙는 키워드로 외부에서 바인딩은 되지만 콜이 안되게 한다
    public event System.Action<Vector3> EventStickMove;
    public event System.Action EventStickUp;
    
    [SerializeField] float m_maxDistance = 500;

    Image m_origin;
    Image m_stick;

    bool m_isStickDown = false;

    Camera m_uiCamera;

    void Start()
    {
        m_origin = transform.Find("Origin").GetComponent<Image>();
        m_stick = transform.Find("Stick").GetComponent<Image>();
        m_uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (m_isStickDown)
        {
            Vector3 dir = m_stick.rectTransform.position -
                m_origin.rectTransform.position;

            if (EventStickMove != null)
            {
                EventStickMove(dir.normalized);//정도에 따른 이속 여기서 손봐야할것 같다.
                //Debug.Log(dir*dir.magnitude / m_maxDistance);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateStick(eventData);
        m_isStickDown = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateStick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
        //스틱에서 손놓았을때
    {
        //스틱의 위치 재정렬
        m_stick.rectTransform.position = m_origin.rectTransform.position;
        m_isStickDown = false;
        //스틱초기화
        m_origin.rectTransform.rotation = new Quaternion(0,0,0,0);
        m_origin.color = new Color(m_origin.color.r, m_origin.color.g, m_origin.color.b, 0);

        if (EventStickUp != null)
            EventStickUp();
    }

    void UpdateStick(PointerEventData eventData)
    {
        //RectTransformUtility!! 이 안에 CameraCanvas관련 함수들이 제공 
        Vector2 result;
        //1.변환할 좌표 결과를 사용할 오브젝트의 부모 Transform
        //2.스크린 좌표
        //3. UICamera 

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            m_uiCamera,
            out result
            );

        Vector3 touchPoint = result;
        Vector3 toTouchPoint = touchPoint - m_origin.rectTransform.localPosition;
        float distance = toTouchPoint.magnitude;

        float rodio = distance / m_maxDistance;
        m_origin.color = new Color(m_origin.color.r, m_origin.color.g, m_origin.color.b, rodio*0.8f);


        Vector3 finalPos;

        if (distance > m_maxDistance)
        {
            finalPos = m_origin.rectTransform.localPosition +
                toTouchPoint.normalized * m_maxDistance;
        }
        else
        {
            finalPos = touchPoint;
        }

        m_stick.rectTransform.localPosition = finalPos;
        //스틱 회전
        float positionX = m_origin.rectTransform.position.x - m_stick.rectTransform.position.x;
        float positionY = m_origin.rectTransform.position.y - m_stick.rectTransform.position.y;
        float angle = Mathf.Atan2(positionY,positionX)*180f/Mathf.PI + 90f;
        //각도
        m_origin.rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        //Debug.Log(m_origin.rectTransform.rotation);
    }

    public Quaternion RetrunPosition()
    {
        return m_origin.rectTransform.rotation;
    }
}