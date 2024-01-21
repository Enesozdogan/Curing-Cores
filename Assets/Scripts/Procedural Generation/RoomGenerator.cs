using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] rooms;

    [SerializeField]
    private Transform[] initalPositions;
    [SerializeField]
    private Transform newLevel;

    private int roomDir;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float roomCd,defaultCd=0.5f;
    [SerializeField]
    private float borderXR,borderXL,borderYD;
    [SerializeField]
    public bool isGenerating = true;
    [SerializeField]
    private int downCount;
    [SerializeField]
    private LayerMask roomDetectionLayer;

    private void Start()
    {
        if (rooms == null)
            return;
        int pos=Random.Range(0,initalPositions.Length);
        transform.position = initalPositions[pos].position;
        var room= Instantiate(rooms[0],transform.position,Quaternion.identity);
        room.transform.parent = newLevel;

        roomDir = Random.Range(1, 6);
    }

    private void Update()
    {
        if (roomCd <= 0 && isGenerating)
        {
            CreateRoom();
            roomCd = defaultCd;

        }
        else
        {
            roomCd -= Time.deltaTime;
        }
      
    }
    private void CreateRoom()
    {
        //yon kontrolu 1,2 sag 3,4 sol, 5 asagi oda uretimi yukaridan basladigi icin sadece asagi yonlu ilerlenebilir.
        if(roomDir==1 || roomDir == 2)
        {
            if (transform.position.x < borderXR)
            {
                downCount = 0;
                Vector2 pos = new Vector2(transform.position.x + offset, transform.position.y);
                transform.position = pos;
                int random = Random.Range(0, rooms.Length);
                var room=Instantiate(rooms[random],transform.position,Quaternion.identity);
                room.transform.parent = newLevel;

                //saga gittikten sonra tekrar ayni odayi yaratmamak icin sola gitmemeli.
                roomDir = Random.Range(1, 6);
                if (roomDir == 3) roomDir = 2;
                else if (roomDir == 4) roomDir = 5;
            }
            else
            {
                roomDir = 5;
            }
        }
        else if(roomDir==3 || roomDir==4)
        {
            if(transform.position.x> borderXL)
            {
                downCount = 0;
                Vector2 pos = new Vector2(transform.position.x - offset, transform.position.y);
                transform.position = pos;

                int random = Random.Range(0, rooms.Length);
                var room = Instantiate(rooms[random], transform.position, Quaternion.identity);
                room.transform.parent = newLevel;

                roomDir = Random.Range(3, 6);
                
            }
            else
            {
                roomDir = 5;
            }

        }
        else
        {
            if (transform.position.y > borderYD)
            {
                downCount++;
                //bir sonraki odanin yerine gitmeden once simdiki odanin alta olan acigi var mi kontrolu yapmak gerekir.
                Collider2D hasOpeningBot = Physics2D.OverlapCircle(transform.position, 1, roomDetectionLayer);
                if (hasOpeningBot == null) return;

                if (hasOpeningBot.GetComponent<RoomHandler>().roomType==roomType.LR || hasOpeningBot.GetComponent<RoomHandler>().roomType == roomType.LRU)
                {
                    hasOpeningBot.GetComponent<RoomHandler>().DestroySelf();

                    if (downCount >= 2)
                    {
                        var room1=Instantiate(rooms[(int)roomType.LRUB],transform.position, Quaternion.identity);
                        room1.transform.parent = newLevel;
                    }
                    else
                    {
                        int randomToGoDown = Random.Range((int)roomType.LRB, (int)roomType.LRUB +1);
                        
                        if (randomToGoDown == (int)roomType.LRU)
                        {
                            randomToGoDown = (int)roomType.LRB;
                            var room1 =Instantiate(rooms[randomToGoDown], transform.position, Quaternion.identity);
                            room1.transform.parent = newLevel;
                        }
                    }
                  
                }
                int random=Random.Range((int)roomType.LRU,(int)roomType.LRUB+1);        
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - offset);
                transform.position = pos;
                var room=Instantiate(rooms[random], transform.position, Quaternion.identity);
                room.transform.parent=newLevel;
                roomDir = Random.Range(1, 6);
            }
            else
            {
                isGenerating = false;
            }
           
        }
       
       
    }
    
}
