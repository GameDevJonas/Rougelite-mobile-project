using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JEnemy : MonoBehaviour
{
    public enum WhatTypeOfEnemy { weak, normal, strong };
    public WhatTypeOfEnemy myType;

    public enum EnemyState { idle, backOff, walking, attack };
    public EnemyState myState = EnemyState.idle;

    public Collider2D myHitbox;
    public Collider2D myRoom;
    public Rigidbody2D rb;

    public AIPath aIPath;
    public AIDestinationSetter destination;

    public HealthSystem healthSystem;
    public PlayerStats pStats;

    public GameObject attackPrefab;
    public GameObject loot;
    public GameObject player;

    private int drop;

    public float speed = 30f;
    public float myHealth;

    public Vector2 dir;
    public float stringDir;
    public Vector2 walkPoint;
    public Vector3 startPos;
    public GameObject startPosO;


    void Start()
    {
        GameObject newStartPosO = Instantiate(startPosO, transform.position, Quaternion.identity, transform.parent);
        startPosO = newStartPosO;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        myRoom = transform.parent.GetComponent<Collider2D>();
        myHitbox = GetComponentInChildren<Collider2D>();

        aIPath = GetComponent<AIPath>();
        destination = GetComponent<AIDestinationSetter>();

        DecideEnemyType();

        myHealth = healthSystem.GetHealth();
        startPos = transform.position;   
    }

    public void DecideEnemyType()
    {
        int randoNumber = Mathf.RoundToInt(Random.Range(0, 3));

        if (randoNumber == 0)
        {
            myType = WhatTypeOfEnemy.weak;
            healthSystem = new HealthSystem(30);
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (randoNumber == 1)
        {
            myType = WhatTypeOfEnemy.normal;
            healthSystem = new HealthSystem(50);
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (randoNumber == 2)
        {
            myType = WhatTypeOfEnemy.strong;
            healthSystem = new HealthSystem(60);
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case EnemyState.idle:
                destination.enabled = false;
                StartCoroutine(IdleState());
                break;
            case EnemyState.walking:
                destination.enabled = true;
                StartCoroutine(WalkState());
                break;
        }
        //stringDir = (transform.position - player.transform.position).normalized;
    }

    public IEnumerator IdleState()
    {
        while (!myRoom.bounds.Contains(player.transform.position))
        {
            yield return new WaitForSeconds(2f);
        }

        myState = EnemyState.walking;

        yield return null;
    }

    public IEnumerator WalkState()
    {
        yield return new WaitForSeconds(Random.Range(0, 3));
        aIPath.enabled = true;
        aIPath.canMove = true;
        while (myRoom.bounds.Contains(player.transform.position)) //Go to player
        {
            yield return new WaitForSeconds(.1f);
            /*walkPoint = (player.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector2(walkPoint.x, walkPoint.y);*/
            destination.target = player.transform;
        }
        while (!myRoom.bounds.Contains(player.transform.position) && !aIPath.isStopped) //Return to startPos
        {
            yield return new WaitForSeconds(.2f);
            /*walkPoint = (startPos - transform.position).normalized * speed;
            rb.velocity = new Vector2(walkPoint.x, walkPoint.y);*/
            //destination.target = startPosO.transform;
            transform.position = startPosO.transform.position;
            aIPath.canMove = false;
            aIPath.enabled = false;
            myState = EnemyState.idle;
        }

        myState = EnemyState.idle;
        aIPath.canMove = false;

        yield return null;
    }
}
