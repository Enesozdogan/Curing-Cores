using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIBase : MonoBehaviour,IAgentController
{
    public Vector2 MovVec { get;  set; }

    public event Action OnActAttack;
    public event Action OnActJumpPressed;
    public event Action OnActJumpReleased;
    public event Action<Vector2> OnActMovement;
    public event Action OnActPerformSkill;
    public event Action OnActRingSwap;

    public void ActivateJumpPressed()
    {
        OnActJumpPressed.Invoke(); 
    }
    public void ActivateJumpReleased()
    {
        OnActJumpReleased.Invoke();
    }
    public void ActivateMovement(Vector2 dir)
    {
        OnActMovement.Invoke(dir);
    }
    public void ActivatePerformSkill()
    {
        OnActPerformSkill.Invoke();
    }
    public void ActivateRingSwap()
    {
        OnActRingSwap.Invoke();
    }
    public void ActivateAttack()
    {
        OnActAttack.Invoke();
    }
}
