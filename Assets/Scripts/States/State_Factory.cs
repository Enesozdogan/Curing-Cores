using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Factory : MonoBehaviour
{
    [SerializeField]
    private StateBase state_idle, state_attack1, state_attack2, state_attack3, state_hook, state_bloodSlash, state_fall, state_jump, state_move,state_teleport,state_getDamaged,state_death,state_roll,state_slam;
    
    public void startStates(AgentScript agentScript)
    {
        StateBase[] states = GetComponentsInChildren<StateBase>();
        foreach (var state in states)
        {
            state.StateInitializer(agentScript);
        }
    }
    public StateBase GetState(stateEnum state_Enum)
        => state_Enum switch
        {
            stateEnum.idle => state_idle,
            stateEnum.attack1 => state_attack1,
            stateEnum.attack2 => state_attack2,
            stateEnum.attack3 => state_attack3,
            stateEnum.hook => state_hook,
            stateEnum.bloodSlash => state_bloodSlash,
            stateEnum.jump => state_jump,
            stateEnum.move => state_move,
            stateEnum.fall => state_fall,
            stateEnum.teleport => state_teleport,
            stateEnum.getDamaged => state_getDamaged,
            stateEnum.death => state_death,
            stateEnum.roll => state_roll,
            stateEnum.slam => state_slam,
            _ => throw new System.Exception("State not found")

        };


    public enum stateEnum
    {
        idle,
        attack1,
        attack2,
        attack3,
        hook,
        bloodSlash,
        fall,
        jump,
        move,
        teleport,
        getDamaged,
        death,
        roll,
        slam

    }
}
