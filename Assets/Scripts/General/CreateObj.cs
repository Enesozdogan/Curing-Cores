using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObj : MonoBehaviour
{
    public GameObject obj;
    public void CreateObject()
    {
        Instantiate(obj,transform.position,Quaternion.identity);
    }
}
