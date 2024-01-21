using System;
using UnityEngine;

public interface IAgentController
{
    Vector2 MovVec { get; }

    event Action OnActAttack;
    event Action OnActJumpPressed;
    event Action OnActJumpReleased;
    event Action<Vector2> OnActMovement;
    event Action OnActPerformSkill;
    event Action OnActRingSwap;
 
}