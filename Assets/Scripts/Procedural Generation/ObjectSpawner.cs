using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] platform_objs;

    private void Awake()
    {
        if(FindObjectOfType<RoomGenerator>() == null)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
         int random=Random.Range(0,platform_objs.Length);
         GameObject objVar = Instantiate(platform_objs[random], transform.position, Quaternion.identity);
         objVar.transform.parent = transform;
    }
   
}
