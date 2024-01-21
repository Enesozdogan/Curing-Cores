using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentScript : MonoBehaviour
{
    
    public Rigidbody2D agentRb2d;
    public AgentAnimManager agentAnimManager;
    [field: SerializeField]
    private UnityEvent OnSpawnNeeded;
    
    public RingManager ringManager;
    [field: SerializeField]
    public UnityEvent OnActDeath;
    public IAgentController inputController;
    public AgentSpriteRenderer agentSpriteRenderer;
    public DmgAndHeal dmgAndHeal;
    public GroundCheck groundCheck;
    public AgentSO agentSO;
    public StateBase state_cur=null,state_prev=null,State_Idle;

    
    [Header("--Weapon--")]
    public Transform weaponTransform;
    public WeaponInfo weaponInfo;
    public LayerMask toBeHit;

    public State_Factory state_factory;
    [SerializeField]
    [Header("--States--")]
    private string nameOfState = "";
    private void Awake()
    {
        agentRb2d = GetComponent<Rigidbody2D>();
        inputController = GetComponentInParent<IAgentController>();
        agentAnimManager = GetComponentInChildren<AgentAnimManager>();
        agentSpriteRenderer = GetComponentInChildren<AgentSpriteRenderer>();
        groundCheck= GetComponentInChildren<GroundCheck>();
        state_factory=GetComponentInChildren<State_Factory>();
        dmgAndHeal=GetComponent<DmgAndHeal>();
        state_factory.startStates(this);
    }

    private void Start()
    {
        
        inputController.OnActMovement += agentSpriteRenderer.FixDirection;
        InitAgentScript();
        
    }
    private void InitAgentScript()
    {
        ShiftToState(State_Idle);
        dmgAndHeal.StartHp(agentSO.health);

    }
  

    internal void ShiftToState(StateBase toState)
    {
        if (toState == null) return;
        if (state_cur != null) state_cur.Exit();
        state_prev = state_cur;
        state_cur = toState;
        state_cur.Enter();
        Show_Situation();
    }

    private void Show_Situation()
    {
        if(state_prev==null|| state_cur.GetType() != state_prev.GetType())
        {
            nameOfState=state_cur.GetType().Name;
        }

    }

    private void Update()
    {
        state_cur.UpdateInState();
    }
    private void FixedUpdate()
    {
        groundCheck.DetectGroundCol();
        state_cur.FixedUpdateState();
    }

    public void AgentDead()
    {
        if (dmgAndHeal.CurrHp > 0)
        {
            OnSpawnNeeded?.Invoke();
        }
        else
        {
            state_cur.HandleDeathAct();
        }
        
    }
    public void GetDamaged()
    {
        state_cur.GetDamaged();
        
    }
}

