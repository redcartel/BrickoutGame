using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntSprite : MonoBehaviour
{
    // Start is called before the first frame update
    
    Transform parentTransform;
    
    GameObject gobj;
    SpriteRenderer sprr;
    Sprite sprite;

    void Start()
    {
        SpriteRenderer existingSprite = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>().sprite;
        existingSprite.enabled = false;
        
        gobj = new GameObject();
        gobj.name = name + " Pixel Aligner";
        gobj.transform.parent = transform;
        gobj.AddComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        gobj.transform.position = new Vector2((int) (transform.position.x +.5f), (int) (transform.position.y + .5f));
    }

    // Update is called once per frame
}
