using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public GameObject aggroRange;
    public GameObject rue;
    public GameObject sword;
    public GameObject attack;

    

    public bool closeEnough = false;
    public bool canAttack = false;
    public bool waiting;
    public bool BackOff = false;
    
    
    Vector2 direction;
    Vector2 walk;
    Vector2 backOff;

    //Stats after class

    public EnemyStats EnemyStats;

    public GameObject commonLoot;
    public GameObject rareLoot;
    public GameObject legendaryLoot;
    public GameObject ancientLoot;
    public GameObject potion;

    public HealthSystem HealthSystem;

    public float health;
    public int damage;
    public int speed;

    public int[] Table;
    public int commondropRange;
    public int raredropRange;
    public int legendarydropRange;
    public int ancientdropRange;
    public int potiondropRange;
    public int none;
    public int randomNumber;
    public int lootTotal;

    private Vector2 dropPoint;

    public bool blocksLight;
    public bool hidesInDark;
    public bool hidesInLight;

    public int level;

    public EnemyType thisType;

    public bool dropping = false;

    //end of Stats after class
    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();

        rue = GameObject.FindGameObjectWithTag("Player");
        //stats in Start()

        thisType = EnemyType.trash;

        level = SceneManager.GetActiveScene().buildIndex;

        EnemyStats = new EnemyStats(thisType, level);


        health = EnemyStats.health;
        damage = EnemyStats.damage;
        speed = EnemyStats.speed;
        HealthSystem = new HealthSystem(health);


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

        //end of stats in Start()*/
    }

    // Update is called once per frame
    void Update()
    {
        health = HealthSystem.GetHealth();

        dropPoint = transform.position;

        if (health == 0 && dropping == false)
        {
            DropLootAndDie();
        }
        
        if (closeEnough == false && waiting == false)
        {
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            walk = (rue.transform.position - transform.position).normalized * speed;
            rb2d.velocity = new Vector2(walk.x, walk.y);
        }
        if (closeEnough == true || waiting == true || BackOff == true && closeEnough == true)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (BackOff == true && closeEnough == false)
        {
            rb2d.velocity = new Vector2(backOff.x, backOff.y);
        }
    }

    //Copy/Paste method in other scripts and use DropLootAndDie() after hp reaches zero or death animations are done.
    private void DropLootAndDie()
    {


        foreach (var item in Table)
        {
            lootTotal += item;
            dropping = true;
        }
        randomNumber = Random.Range(0, (lootTotal + 1));


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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            backOff = (-collision.gameObject.transform.position + transform.position).normalized * speed;
            BackOff = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BackOff = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == "Sword" || collider.tag == "Arrow")
        {
            if(collider.tag == "Arrow")
            {
                Debug.Log("arrow hit");
            }
            PlayerStats playerstats = rue.GetComponent<PlayerStats>();
            Player player = rue.GetComponent<Player>();
            Swordscript swordCrit = collider.GetComponent<Swordscript>();
            Arrow arrowCrit = collider.GetComponent<Arrow>();
            if (swordCrit.Crit == false || arrowCrit.crit == false)
            {
                HealthSystem.Damage(playerstats.Strength.Value * playerstats.SwordAttackModifier.Value);
                player.HealthSystem.Heal(playerstats.LifeOnHit.Value);
            }
            if (swordCrit.Crit == true || arrowCrit.crit == true)
            {
                HealthSystem.Damage((playerstats.Strength.Value * playerstats.SwordAttackModifier.Value) * (playerstats.CritDamage.Value / 100));
                player.HealthSystem.Heal(playerstats.LifeOnHit.Value);
            }


        }
        
    }

    public void Attack()
    {
        waiting = true;
        canAttack = false;
        direction = rue.transform.position - transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        GameObject clonedObject = Instantiate(attack, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), transform);
        Destroy(clonedObject, 0.3f);
        Invoke("Check", 0.5f);
    }
    public void Check()
    {
        
        if (closeEnough == true && canAttack == true)
        {
            waiting = true;
            Invoke("Attack", 0.5f);
        }
        if (closeEnough == false || canAttack == false)
        {
            waiting = false;
            Invoke("Wait", 0.5f);
        }
    }
    public void Wait()
    {
        waiting = true;
        canAttack = true;
        Check();
    }

    public void Damage()
    {
        PlayerStats playerstats = rue.GetComponent<PlayerStats>();
        HealthSystem.Damage(playerstats.Strength.Value);
    }
}
