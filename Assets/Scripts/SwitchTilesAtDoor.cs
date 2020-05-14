using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTilesAtDoor : MonoBehaviour
{
    public Sprite notDoorSprite;
    public Sprite doorSprite;

    public bool isThereDoor = false;
    void Start()
    {
        //Debug.Log("", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isThereDoor)
        {
            GetComponent<SpriteRenderer>().sprite = doorSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = notDoorSprite;
        }
    }

    public void SwitchTile()
    {
        
    }
}
