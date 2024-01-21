using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
    [SerializeField]
    DialogScript_Base dialog=null;
    [SerializeField]
    string speakerName;
    DialogConnector connector;
    PlayerInputController inputController;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite=GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialog == null || !collision.CompareTag("Player")) return;
        Cursor.visible = true;
        inputController= collision.gameObject.GetComponentInParent<PlayerInputController>();
        inputController.isInDialog= true;
        connector = collision.gameObject.GetComponentInParent<DialogConnector>();
        connector.isInDialog = true;
        connector.speakerName= speakerName;
        connector.IniaiteDialog(dialog);
        if (transform.position.x > collision.transform.position.x)
        {
            sprite.flipX= true;
        }
        else if(transform.position.x < collision.transform.position.x)
        {
            sprite.flipX = false;
        }
       
            GetComponentInChildren<Animator>().SetBool("IsTalking", true);
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Cursor.visible = false;
        inputController.isInDialog = false;
        connector.isInDialog= false;
        connector.FinishDialog();
        GetComponentInChildren<Animator>().SetBool("IsTalking", false);
    }

}
