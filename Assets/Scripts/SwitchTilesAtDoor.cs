using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTilesAtDoor : MonoBehaviour
{
    public Sprite notDoorSprite;
    public Sprite doorSprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTile(bool isThereDoor)
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
}
