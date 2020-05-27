using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Sprite[] obstacleSprites;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randNumb = Random.Range(0, 5);
        rend.sprite = obstacleSprites[randNumb];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
