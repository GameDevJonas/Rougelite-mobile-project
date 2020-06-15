using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenBossPlayer : MonoBehaviour
{
    Transform boss, player;
    public float distance;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = (boss.position + player.position) / 2;
        transform.position = myPos;

        distance = (boss.position - player.position).magnitude;
    }
}
