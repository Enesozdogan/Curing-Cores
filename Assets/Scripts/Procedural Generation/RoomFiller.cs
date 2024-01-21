using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFiller : MonoBehaviour
{
    [SerializeField]
    RoomGenerator roomGenerator;
    [SerializeField]
    Transform newLevel;
    [SerializeField]
    private LayerMask rooms;
    private void Awake()
    {
        if (FindObjectOfType<RoomGenerator>() == null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        Collider2D roomDetector = Physics2D.OverlapCircle(transform.position, 1, rooms);
       
        if (roomDetector == null && !roomGenerator.isGenerating)
        {
            int rand = Random.Range((int)roomType.LR, (int)roomType.LRUB + 1);
            var room=Instantiate(roomGenerator.rooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = newLevel;
       
            
        }
       
    }
}
