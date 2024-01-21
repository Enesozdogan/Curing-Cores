using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoAnim : MonoBehaviour
{
    [SerializeField]
    private float movDist = 0.4f;
    [SerializeField]
    private float animDur = 1.2f;
    [SerializeField]
    private Ease animEase;

    private void Start()
    {
        transform
            .DOMoveY(transform.position.y+movDist,animDur)
            .SetEase(animEase)
            .SetLoops(-1,LoopType.Yoyo);
    }
    
    private void OnDisable()
    {
        DOTween.Pause(transform);
    }
    private void OnEnable()
    {
        DOTween.Restart(transform);

    }



}
