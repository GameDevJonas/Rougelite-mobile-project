using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffColliderWhenNotVisisble : MonoBehaviour
{
    public Collider2D myCol;
    public SpriteRenderer myRend;

    void Start()
    {
        if (GetComponent<Collider2D>())
        {
            myCol = GetComponent<Collider2D>();
        }
        if (GetComponent<SpriteRenderer>())
        {
            myRend = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        if(myCol != null)
        {
            myCol.enabled = true;
        }
        if(myRend != null)
        {
           // myRend.enabled = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (myCol != null)
        {
            myCol.enabled = false;
        }
        if (myRend != null)
        {
            //myRend.enabled = false;
        }
    }
}
