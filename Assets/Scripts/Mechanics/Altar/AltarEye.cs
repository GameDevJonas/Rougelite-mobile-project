using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarEye : MonoBehaviour
{
    GameObject player;
    public string direction;
    public Sprite playerIsUp, playerIsLeft, playerIsRight, playerIsDown;
    SpriteRenderer myRend;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirFromPlayer();
    }

    public void GetDirFromPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        int dirTwo = Mathf.RoundToInt(dir.x);
        int dirThree = Mathf.RoundToInt(dir.y);
        Vector2 dirVector = new Vector2(dirTwo, dirThree);

        if (dirVector == new Vector2(0f, 1f))
        {
            direction = "U";
            myRend.sprite = playerIsUp;
        }
        else if (dirVector == new Vector2(0f, -1f))
        {
            direction = "D";
            myRend.sprite = playerIsDown;
        }
        else if (dirVector == new Vector2(1f, 0f))
        {
            direction = "R";
            myRend.sprite = playerIsRight;
        }
        else if (dirVector == new Vector2(-1f, 0f))
        {
            direction = "L";
            myRend.sprite = playerIsLeft;
        }
    }
}
