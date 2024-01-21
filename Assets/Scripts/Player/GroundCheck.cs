using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool isGrounded;
    [SerializeField]
    private Collider2D colAgent;
    [Header("Offset Values")]
    [Range(-3f, 3f)]
    public float offSetX;
    [Range(-3f, 3f)]
    public float offSetY;

    public float widthCol,heightCol;

    public Color colorGround = Color.green, colorNotGrounded = Color.red;


    private void Awake()
    {
        if (colAgent == null)
            colAgent= GetComponent<Collider2D>();
    }
   
    public void DetectGroundCol()
    {
        RaycastHit2D hit = Physics2D.BoxCast(colAgent.bounds.center + new Vector3(offSetX, offSetY, 0), new Vector3(widthCol, heightCol, 0), 0, Vector2.down, 0, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else isGrounded= false;
    }
    private void OnDrawGizmos()
    {
        if (colAgent == null) return;
        Gizmos.color = colorNotGrounded;
        if (isGrounded) Gizmos.color = colorGround;
        

        Gizmos.DrawWireCube(colAgent.bounds.center+new Vector3(offSetX,offSetY,0),new Vector3(widthCol,heightCol,0));
    }
}
