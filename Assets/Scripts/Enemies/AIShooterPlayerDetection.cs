using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooterPlayerDetection : MonoBehaviour
{
    [field:SerializeField]
    public bool isDetected { get; private set; }
    public LayerMask playerMask;
    [SerializeField]
    private Transform detectPos;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private float delay = 0.3f;

    [SerializeField]
    private Color normalColor=Color.green,aggroColor=Color.red;
    [SerializeField]
    private bool gizmoOn = true;
    public Vector2 dir=> player.transform.position-detectPos.position;
    private GameObject player;

    private void Start()
    {
        StartCoroutine(detectPlayer());
    }
    public GameObject Player
    {
        get { return player; }
        private set
        {
            player = value;
           
            if (player != null)
            {
                isDetected = true;
            }
            else
            {
                isDetected = false;
            }
        }
    }

    
    IEnumerator detectPlayer()
    {
        yield return new WaitForSeconds(delay);

        var col = Physics2D.OverlapBox((Vector2)detectPos.position + offset, size, 0, playerMask);
        if(col != null)
        {
            Player = col.gameObject;
        }
        else
        {
            Player = null;
        }
        StartCoroutine(detectPlayer());
    }
    private void OnDrawGizmos()
    {
        if (gizmoOn)
        {
            Gizmos.color = normalColor;
            if (isDetected)
            {
                Gizmos.color=aggroColor;
            }
            Gizmos.DrawCube((Vector2)detectPos.position + offset, size);
        }
    }

}
