using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    readonly int m_animeHashKeyState = Animator.StringToHash("State");
    readonly int m_animeHashKeyIsDamage = Animator.StringToHash("IsDamage");//bool
    readonly int m_animeHashKeyDie = Animator.StringToHash("Die");//트리거

    enum State : int
    {
        Standard = 0,
        Follow = 1,
        Attack = 2,
    }
    State m_state;
    bool m_isDamage = false;
    [SerializeField] int m_hp = 100;
    [SerializeField] int m_attackPawer = 10;

    GameObject m_playerController;
    NavMeshAgent m_navMeshAgent;
    Animator m_animator;
    [SerializeField] Transform m_monsterWeapon;

    private void Awake()
    {
        //m_playerController = GameObject.Find("PlayerController");
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }
    private void Start()
    {
        m_playerController = GameManager.instace.FindObject("PlayerController");
        m_monsterWeapon.gameObject.SetActive(false);
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, m_playerController.transform.position);
        Debug.Log("몬스터와의 거리 : "+distance);
        if (distance < 2)
        {
            ChangeState(State.Attack);
            transform.LookAt(m_playerController.transform);
        }
        else if (distance < 10)
        {
            ChangeState(State.Follow);

        }
        else if (distance > 10)
        {
            ChangeState(State.Standard);
        }

        //탐색 횟수를 줄일 필요있음
        //m_navMeshAgent.SetDestination(m_playerController.transform.position);
        //transform.LookAt(m_playerController.transform);
    }
    private void ChangeState(State state)
    {
        if (m_state == state)
            return;
        m_state = state;
        m_animator.SetInteger(m_animeHashKeyState, (int)m_state);

        //전환 될 때 한번만 실행되는 것
        switch(m_state)
        {
            case State.Standard:
                m_monsterWeapon.gameObject.SetActive(false);
                m_navMeshAgent.enabled = false;
                break;
            case State.Follow:
                m_monsterWeapon.gameObject.SetActive(true);
                m_navMeshAgent.enabled = true;
                m_navMeshAgent.SetDestination(m_playerController.transform.position);
                break;
            case State.Attack:
                break;
        }
    }
    public void Damege(int damege)
    {
        if (m_isDamage == false)
        {
            m_hp -= damege;
            m_isDamage = true;
            m_animator.SetBool(m_animeHashKeyIsDamage, m_isDamage);
        }
        if(m_hp<0)
        {
            m_animator.SetTrigger(m_animeHashKeyDie);
        }
    }

    void OnAnimationDamegeEnd()
    {
        ChangeState(State.Standard);
        m_isDamage = false;
        m_animator.SetBool(m_animeHashKeyIsDamage, m_isDamage);
    }

    void OnAnimationEnd()
    {
        ChangeState(State.Standard);
    }

}
