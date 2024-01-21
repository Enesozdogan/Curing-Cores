using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFxPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject bloodPrefab;
    [SerializeField]
    float offset=1;
    

    public void CreateBlood()
    {
       GameObject go=Instantiate(bloodPrefab,transform.position+new Vector3(offset*(-transform.localScale.x),0,0),Quaternion.identity);
        go.transform.localScale = -transform.localScale;
       go.transform.parent = transform;
       
        Destroy(go, 0.5f);
    }
}
