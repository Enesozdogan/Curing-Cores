using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LvlTrasitionier : MonoBehaviour
{
    [SerializeField]
    bool playerContact = false;
    public UnityEvent OnCollisionTransitioner;
    [SerializeField]
    GameObject progressCanvas;
    private void Update()
    {
        if(playerContact )
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnCollisionTransitioner.Invoke();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerContact = true;
            progressCanvas.SetActive(true);
        }
     
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerContact = false;
            progressCanvas.SetActive(false);
        }
        
    }
}
