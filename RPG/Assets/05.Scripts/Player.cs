using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInfo m_playerInfo;
    public PlayerInfo playerInfo { set { m_playerInfo = value; } get { return m_playerInfo; } } 
    // 벨류로 들어가니 포인터 로 넣어줘야 할까?
    readonly int m_animeHashKeyState = Animator.StringToHash("State");
    readonly int m_animeHashKeyIsRun = Animator.StringToHash("IsRun");
    readonly int m_animeHashKeyBackFront = Animator.StringToHash("BackFront");
    readonly int m_animeHashKeyLeftRight = Animator.StringToHash("LeftRight");
    readonly int m_animeHashKeyCombo = Animator.StringToHash("ComboCount");
    readonly int m_animeHashKeyIsReadyCombo = Animator.StringToHash("IsReadyCombo");
    readonly int m_animeHashKeyIsDamege = Animator.StringToHash("Damege");
    readonly int m_animeHashKeyDie = Animator.StringToHash("Die");

    CapsuleCollider[] m_weapons;
    bool m_readyCombo = false;
    int m_combo = 0;
    bool m_isRun=false;
    bool m_isDamege = false;
    
    enum State : int
    {
        Standard = 0,
        Attack = 1,
        Skill1 = 2,
        Jump = 3,
        Damege = 4,
        Die = 5
    }
    State m_state = 0;
    Animator m_animator;

    [SerializeField] float m_speed = 5f;
    [SerializeField] float m_rotateSpeed = 10f;
    

    private void Awake()
    {
        
        //조이스틱 바인딩
        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickMove += OnEventStictMove;
        GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickUp += OnEventStickUp;
        //스킬 바인딩
        GameObject.Find("PanelSkill").GetComponent<PanelSkillButton>().EventPlaySkill += OnSkillPlay;

        m_weapons = transform.Find("全ての親").GetComponentsInChildren<CapsuleCollider>();
        for(int i=0; i<m_weapons.Length; ++i)
        {
            m_weapons[i].enabled = false;
        }
    }
    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (m_state == State.Jump)
        {
            if(GetComponent<Rigidbody>().velocity.y>0)
            {

            }
            else if(GetComponent<Rigidbody>().velocity.y==0)
            {

            }
            else if(GetComponent<Rigidbody>().velocity.y<0)
            {

            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    //정지시
    //    //if(m_state == State.Jump && other.gameObject.layer==LayerMask.NameToLayer("Ground"))
    //    //{
    //    //    
    //    //}
    //}

    //private void OnDestroy()
    //{
    //    GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickMove -= OnEventStictMove;
    //    GameObject.Find("PanelJoystick").GetComponent<PanelJoystick>().EventStickUp -= OnEventStickUp;
    //}
    //조이스틱 관련조작
    void OnEventStictMove(Vector3 dir)
    {
        //이동
        Vector3 worldDir = new Vector3();
        worldDir.x = dir.x*10f;
        worldDir.y = 0f;
        worldDir.z = dir.y*10f;

        Vector3 finalDir =
            //카메라기준으로 보는것
            Camera.main.transform.TransformDirection(worldDir)/10;

        if (finalDir != Vector3.zero 
            //Standard일때만 이동
            && m_state == State.Standard)
        {
            //회전
            //Quaternion targetRotation = Quaternion.LookRotation(finalDir.normalized);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_rotateSpeed * Time.deltaTime);
            //Debug.Log("targetRotation :     " + targetRotation);
            //Debug.Log("transform.rotation : " + transform.localRotation);
            finalDir.y = 0f;
            //이동은 컨트롤러에서 해준다.
            //Move(finalDir);

            //애니메이션
            m_animator.SetFloat(m_animeHashKeyLeftRight, finalDir.x);
            m_animator.SetFloat(m_animeHashKeyBackFront, finalDir.z);
        }
    }
    void OnEventStickUp()
    {
        //z축 초기화-후에 부드럽게 바꿔볼것-이동 관련은 컨트롤러에서
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);

        m_animator.SetFloat(m_animeHashKeyBackFront, 0f);
        m_animator.SetFloat(m_animeHashKeyLeftRight, 0f);
    }
    private void ChangeState(State state)
    {
        if (m_state == state)
            return;
        m_state = state;

        m_animator.SetInteger(m_animeHashKeyState, (int)m_state);

        switch (m_state)
        {
            case State.Standard:
                m_readyCombo = true;
                m_animator.SetBool(m_animeHashKeyIsReadyCombo, m_readyCombo);
                break;
            case State.Attack:
                break;
            case State.Skill1:
                break;
            case State.Jump:
                break;
        }
    }
    //private void Move(Vector3 dir) - 컨트롤러에서 이동 관련해줌
    //{
    //    transform.Translate(dir * m_speed * Time.deltaTime, Space.World);
    //}

    //스킬버튼 눌렀을때 호출되는 함수
    void OnSkillPlay(SkillInfo skillInfo)
    {
        if (skillInfo.Name == "Attack")
        {
            m_combo++;
            m_animator.SetInteger(m_animeHashKeyCombo, m_combo);
            Debug.Log("플레이어에서 스킬 시동");
            //if (m_readyCombo == true)
            //{
                //m_isCombo = true;
                //m_animator.SetBool(m_animeHashKeyIsCombo, m_isCombo);
            //}
            ChangeState(State.Attack);
        }
        else if (skillInfo.Name == "Jump")
        {
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
            ChangeState(State.Jump);
        }
        else if (skillInfo.Name == "Dush")
        {

        }
        else if(skillInfo.Name == "Skill1")
        {

        }
    }

    void OnAnimationEnd()
        //무슨 애니메이션이든 Standard로 전환시 할것
    {
        ChangeState(State.Standard);
        m_readyCombo = false;
        m_animator.SetBool(m_animeHashKeyIsReadyCombo, m_readyCombo);
        m_combo = 0;
        m_animator.SetInteger(m_animeHashKeyCombo, m_combo);
        if (m_isDamege)
        {
            m_isDamege = false;
        }
        //활성화
        for (int i = 0; i < m_weapons.Length; ++i)
        {
            m_weapons[i].enabled = false;
        }
    }
    void OnAnimationAttectStart()
    {
        m_combo--;
        m_animator.SetInteger(m_animeHashKeyCombo, m_combo);
        m_readyCombo = false;
        m_animator.SetBool(m_animeHashKeyIsReadyCombo, m_readyCombo);
        //어택에니메이션 시작시 콤보 초기화
        //m_isCombo = false;
        //m_animator.SetBool(m_animeHashKeyIsCombo, m_isCombo);
        //활성화
        for (int i = 0; i < m_weapons.Length; ++i)
        {
            m_weapons[i].enabled = false;
        }
    }
    void OnAnimationAttactEnd()
    {
        m_readyCombo = true;
        m_animator.SetBool(m_animeHashKeyIsReadyCombo, m_readyCombo);
    }

    public void Damege(int attackDamege)
    {
        if (m_isDamege == false)
        {
            m_playerInfo.Hp -= attackDamege;
            m_animator.SetTrigger(m_animeHashKeyIsDamege);
            UIManager.instace.GeneratDamageText(transform, attackDamege, false);
        }
        if (m_playerInfo.Hp < 0)
        {
            m_animator.SetTrigger(m_animeHashKeyDie);
        }
    }
    
}
