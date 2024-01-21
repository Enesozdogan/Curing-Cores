using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlock : MonoBehaviour,IToBeHit
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    private int i = 0;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    public void GetDamaged(GameObject go, int dmg)
    {
        spriteRenderer.sprite = sprites[i++];
        if (i == sprites.Count)
        {
            Destroy(this.gameObject);
        }
    }
}
