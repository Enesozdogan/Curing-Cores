using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoTween : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPos;
    [SerializeField]
    float time;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        player=FindObjectOfType<PlayerInputController>().gameObject;
    }
    void Start()
    {
        transform.DOMove(targetPos, time)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InSine);
    }
    private void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent= transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = player.transform;
    }
    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}