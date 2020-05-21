using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class JEnemy : MonoBehaviour
{
    public EnemyStats EnemyStats;
    public EnemyType thisType;

    //private enum WhatTypeOfEnemy { weak, normal, strong };
    //private WhatTypeOfEnemy myType;

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

    public float speed = 30f;
    public float myHealth;
    public float debugHealth;
    public float damage;


    public Vector2 dir;
    public float stringDir;
    public Vector2 walkPoint;
    public Vector3 startPos;
    public GameObject startPosO;

    public bool blocksLight;
    public bool hidesInDark;
    public bool hidesInLight;
    public bool attacked;

    public int level;

    #region Loot drops
    [Header("Loot dropping")]
    private int drop;
    public int[] Table;
    public int commondropRange;
    public int raredropRange;
    public int legendarydropRange;
    public int ancientdropRange;
    public int potiondropRange;
    public int none;
    public int lootTotal;

    public GameObject commonLoot;
    public GameObject rareLoot;
    public GameObject legendaryLoot;
    public GameObject ancientLoot;
    public GameObject potion;

    public bool dropping = false;
    #endregion


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

        //DecideEnemyType();

        startPos = transform.position;

        thisType = EnemyType.trash;
        level = SceneManager.GetActiveScene().buildIndex;
        EnemyStats = new EnemyStats(thisType, level);

        myHealth = EnemyStats.health;
        damage = EnemyStats.damage;
        speed = EnemyStats.speed;
        healthSystem = new HealthSystem(myHealth);


        Table = EnemyStats.Table;
        commondropRange = EnemyStats.commondropRange;
        raredropRange = EnemyStats.raredropRange;
        legendarydropRange = EnemyStats.legendarydropRange;
        ancientdropRange = EnemyStats.ancientdropRange;
        potiondropRange = EnemyStats.potiondropRange;
        none = EnemyStats.none;
        lootTotal = EnemyStats.lootTotal;

        blocksLight = EnemyStats.blocksLight;
        hidesInDark = EnemyStats.hidesInDark;
        hidesInLight = EnemyStats.hidesInLight;

        aIPath.maxSpeed = speed;
    }

    /*public void DecideEnemyType()
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
    }*/

    // Update is called once per frame
    void Update()
    {
        myHealth = healthSystem.GetHealth();

        attacked = false;
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
            case EnemyState.attack:
                AttackState();
                break;
                
        }

        if(myHealth <= 0 && !dropping)
        {
            DropLootAndDie();
        } //ded
    }

    public void AttackState()
    {

    }

    private void DropLootAndDie()
    {
        foreach (var item in Table)
        {
            lootTotal += item;
            dropping = true;
        }
        float randomNumber = Random.Range(0, (lootTotal + 1));

        foreach (var weight in Table)
        {
            if (randomNumber <= weight)
            {


                if (weight == commondropRange)
                {
                    Instantiate<GameObject>(commonLoot, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;

                }

                if (weight == raredropRange)
                {
                    Instantiate<GameObject>(rareLoot, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }

                if (weight == legendarydropRange)
                {
                    Instantiate<GameObject>(legendaryLoot, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }

                if (weight == ancientdropRange)
                {
                    Instantiate<GameObject>(ancientLoot, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }

                if (weight == potiondropRange)
                {
                    Instantiate<GameObject>(potion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }

                if (weight == none)
                {
                    Destroy(gameObject);
                    return;
                }
            }

            else

            {
                randomNumber -= weight;
            }
        }
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
            if (aIPath.reachedEndOfPath)
            {
                myState = EnemyState.attack;
            }
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

        Debug.Log("Walkstate is fin");
        myState = EnemyState.idle;
        aIPath.canMove = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Sword" || collider.tag == "Arrow")
        {
            if (attacked)
            {
                return;
            }
            if (collider.tag == "Arrow")
            {
                PlayerStats playerstats = player.GetComponent<PlayerStats>();
                Player rue = player.GetComponent<Player>();
                Arrow arrowCrit = collider.GetComponent<Arrow>();
                if (!arrowCrit.crit)
                {
                    float damage = playerstats.Strength.Value * playerstats.CrossbowAttackModifier.Value;
                    healthSystem.Damage(damage);
                    rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                    Debug.Log("Crossbow hit: " + damage);
                    attacked = true;
                }
                else if (arrowCrit.crit)
                {
                    float damage = (playerstats.Strength.Value * playerstats.CrossbowAttackModifier.Value) * (playerstats.CritDamage.Value / 100);
                    healthSystem.Damage(damage);
                    rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                    Debug.Log("Crossbow hit with crit: " + damage);
                    attacked = true;
                }
            }
            else if (collider.tag == "Sword")
            {
                PlayerStats playerstats = player.GetComponent<PlayerStats>();
                Player rue = player.GetComponent<Player>();
                Swordscript swordCrit = collider.GetComponent<Swordscript>();
                if (!swordCrit.Crit)
                {
                    float damage = playerstats.Strength.Value * playerstats.SwordAttackModifier.Value;
                    healthSystem.Damage(damage);
                    rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                    Debug.Log("Sword damaged: " + damage);
                    attacked = true;
                }
                else if (swordCrit.Crit)
                {
                    float damage = (playerstats.Strength.Value * playerstats.SwordAttackModifier.Value) * (playerstats.CritDamage.Value / 100);
                    healthSystem.Damage(damage);
                    rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                    Debug.Log("Sword damaged with crit: " + damage);
                    attacked = true;
                }
            }
        }
    } //Deal damage to enemy
}
