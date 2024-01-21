using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSOTuner : MonoBehaviour,ILoadAndSave
{
    private AgentScript agent;
    public UnityEvent OnActPowerPickUp, OnActSpeedPickUp, OnActTpPickUp;
    [Header("Low Limit Values")]
    [SerializeField]
    private float minValUpForce;
    [SerializeField]
    private float minValSpeedMax;
    [SerializeField]
    private float minValGravityMultj;
    [SerializeField]
    private float minTeleportVal;
    [SerializeField]
    private int minDmgVal;
    [Header("Top Limit Values")]
    [SerializeField]
    private float maxValUpForce;
    [SerializeField]
    private float maxValSpeedMax;
    [SerializeField]
    private float maxValGravityMultj;
    [SerializeField]
    private float maxTeleportVal;
    [SerializeField]
    private int maxDmgVal;
    private void Awake()
    {
        agent = GetComponent<AgentScript>();
    }
    public void IncreasePower(int incAmount)
    {
        if(agent == null) { return; }

        agent.weaponInfo.damage += incAmount;
        agent.weaponInfo.damage = Mathf.Clamp(agent.weaponInfo.damage, minDmgVal, maxDmgVal);
        OnActPowerPickUp?.Invoke();

    }
    public void IncreasePlatforming(float incAmount)
    {
        if(agent == null) { return; }
        agent.agentSO.upForce += incAmount/4;
        agent.agentSO.gravityMultJ += incAmount/4;
        agent.agentSO.speedMax += incAmount;
        agent.agentSO.upForce = Mathf.Clamp(agent.agentSO.upForce, minValUpForce, maxValUpForce);
        agent.agentSO.gravityMultJ = Mathf.Clamp(agent.agentSO.gravityMultJ, minValGravityMultj, maxValGravityMultj);
        agent.agentSO.speedMax = Mathf.Clamp(agent.agentSO.speedMax, minValSpeedMax, maxValSpeedMax);
        OnActSpeedPickUp?.Invoke();
    }
    public void IncreaseTeleport(float incAmount)
    {
        if(agent == null) { return; }
        agent.agentSO.teleportDist+= incAmount;
        agent.agentSO.teleportDist = Mathf.Clamp(agent.agentSO.teleportDist, minTeleportVal, maxTeleportVal);
        OnActTpPickUp?.Invoke();
    }

    public void Save()
    {
        agent.ringManager.rings[agent.ringManager.index].OnExit();
        
        SaveAndLoadFuns.SaveSpeedVal(agent.agentSO.speedMax);
        SaveAndLoadFuns.SaveStrVal(agent.weaponInfo.damage);
        SaveAndLoadFuns.SaveTpVal(agent.agentSO.teleportDist);
        SaveAndLoadFuns.SaveGravityJVal(agent.agentSO.gravityMultJ);
        SaveAndLoadFuns.SaveUpForceVal(agent.agentSO.upForce);
        SaveAndLoadFuns.SaveRingVal(agent.ringManager.index);
        
    }

    public void Load()
    {
        
        
        

        agent.agentSO.speedMax = SaveAndLoadFuns.LoadSpeedVal();
        agent.weaponInfo.damage = SaveAndLoadFuns.LoadStrVal();
        agent.agentSO.teleportDist = SaveAndLoadFuns.LoadTpVal();
        agent.agentSO.gravityMultJ = SaveAndLoadFuns.LoadGravityJVal();
        agent.agentSO.upForce = SaveAndLoadFuns.LoadUpForceVal();
        
        agent.ringManager.ApplyRingAttributes(0);
        SetDefaultValues();


    }
      
       


    private void SetDefaultValues()
    {
        if (agent.weaponInfo.damage == 2) agent.weaponInfo.damage = 5;
        if (agent.agentSO.speedMax == 0) agent.agentSO.speedMax = 6;
        if (agent.agentSO.teleportDist == 0) agent.agentSO.teleportDist = 20;
        if (agent.agentSO.gravityMultJ == 0) agent.agentSO.gravityMultJ = 1.5f;
        if (agent.agentSO.upForce == 0) agent.agentSO.upForce = 10;
    }
}
