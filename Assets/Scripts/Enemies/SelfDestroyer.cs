using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public void TerminateSelf()
    {
        Destroy(gameObject);
    }
    public void TerminateObj(GameObject obj)
    {
        Destroy(obj.gameObject);
    }

}
