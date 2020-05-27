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


    public float dirRotation;
    public string direction;
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

        GetDirFromPlayer();

        if (myHealth <= 0 && !dropping)
        {
            DropLootAndDie();
        } //ded
    }

    public void GetDirFromPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        int dirTwo = Mathf.RoundToInt(dir.x);
        int dirThree = Mathf.RoundToInt(dir.y);
        Vector2 dirVector = new Vector2(dirTwo, dirThree);
        //Debug.Log("OG number = " + dir.x + " normalized number = " + dirTwo + ", " + "OG number = " + dir.y + " normalized number = " + + dirThree);
        //Debug.Log("x: " + dirTwo + " y: " + dirThree);
        if (myState != EnemyState.attack)
        {
            if (dirVector == new Vector2(0f, 1f))
            {
                direction = "U";
                dirRotation = 0;
            }
            else if (dirVector == new Vector2(0f, -1f))
            {
                direction = "D";
                dirRotation = 180;
            }
            else if (dirVector == new Vector2(1f, 0f))
            {
                direction = "R";
                dirRotation = 270;
            }
            else if (dirVector == new Vector2(-1f, 0f))
            {
                direction = "L";
                dirRotation = 90;
            }
        }
    }

    public void AttackState()
    {
        StopAllCoroutines();
        aIPath.enabled = false;
        aIPath.canMove = false;
        GameObject attackClone = Instantiate(attackPrefab, transform.position, Quaternion.Euler(0, 0, dirRotation), transform); //Add an attackPoint
        Destroy(attackClone, 0.3f);
        myState = EnemyState.idle;
    }

    private void DropLootAndDie()
    {
        foreach (var item in Table) //checks table
        {
            lootTotal += item;
            dropping = true;
        }
        float randomNumber = Random.Range(0, (lootTotal + 1)); //pulls random number based on table total + 1

        foreach (var weight in Table) //weight, is the number listed in the table of drop chance.
        {
            if (randomNumber <= weight) //if less or equal to a weight, give item
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

            else //if not, roll -= highest value weight.

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

        yield return new WaitForSeconds(3f);
        myState = EnemyState.walking;

        yield return null;
    }

    public IEnumerator WalkState()
    {
        StopCoroutine(IdleState());
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
                yield return new WaitForSeconds(3f);
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
        aIPath.canMove = false;
        myState = EnemyState.idle;

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
                    float damage = playerstats.Strength.Value * playerstats.CrossbowAttackModifier.Value; //deals damage to this enemy and heals player.
                    healthSystem.Damage(damage);
                    rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                    Debug.Log("Crossbow hit: " + damage);
                    attacked = true;
                }
                else if (arrowCrit.crit) //same but critically striked for more damage.
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
