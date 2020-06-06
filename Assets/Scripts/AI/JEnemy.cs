using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class JEnemy : MonoBehaviour
{
    float overlapRange = 15f;
    public LayerMask mask;

    public EnemyStats EnemyStats;
    public EnemyType thisType;

    //private enum WhatTypeOfEnemy { weak, normal, strong };
    //private WhatTypeOfEnemy myType;

    public enum EnemyState { non, idle, backOff, walking, attack, damage, dead };
    public EnemyState myState = EnemyState.non;

    public Collider2D myHitbox;
    public Collider2D myRoom;
    public Rigidbody2D rb;

    public AIPath aIPath;
    public AIDestinationSetter destination;

    public HealthSystem healthSystem;

    public GameObject damagePopup;
    public TextMeshProUGUI dmgtext;
    public GameObject attackPrefab;
    public GameObject loot;
    public GameObject player;
    public GameObject destroyChildren;

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
    public bool hasAttacked = false;
    private bool isDead = false;
    bool nonStateChecker = true;

    public int level;

    public int backOffCounter = 0;

    public Transform pointA, pointB, pointC, pointD;

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
    public bool dropping;

    public GameObject commonLoot;
    public GameObject rareLoot;
    public GameObject legendaryLoot;
    public GameObject ancientLoot;
    public GameObject potion;
    #endregion

    #region Animation
    Animator anim;
    bool isIdle;
    bool isWalking;
    bool isHurt;
    bool isAttacking;
    bool playHurtAnim, playAttackAnim;
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

        pointA = GetComponentInChildren<Animation>().transform;
        pointB = GetComponentInChildren<BoxCollider>().transform;
        pointC = GetComponentInChildren<MeshFilter>().transform;
        pointD = GetComponentInChildren<SphereCollider>().transform;

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

        anim = GetComponentInChildren<Animator>();
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
    public void GetDirFromPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        int dirTwo = Mathf.RoundToInt(dir.x);
        int dirThree = Mathf.RoundToInt(dir.y);
        Vector2 dirVector = new Vector2(dirTwo, dirThree);
        //Debug.Log("OG number = " + dir.x + " normalized number = " + dirTwo + ", " + "OG number = " + dir.y + " normalized number = " + + dirThree);
        //Debug.Log("x: " + dirTwo + " y: " + dirThree);
        if (myState != EnemyState.attack && !isDead)
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

    void Anims()
    {
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsIdle", isIdle);
        anim.SetBool("HasAttackes", hasAttacked);

        if (direction == "L")
        {
            Transform animScale = anim.gameObject.transform;
            animScale.localScale = new Vector3(-1, 1, 1);
        }
        if (direction == "R")
        {
            Transform animScale = anim.gameObject.transform;
            animScale.localScale = new Vector3(1, 1, 1);
        }
    }

    void ResetAnimForAttacknHurt(int i)
    {
        if(i == 0) //Hurt anim
        {

        }
        else if(1 == 1) //Attack anim
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        myHealth = healthSystem.GetHealth();

        attacked = false;
        switch (myState)
        {
            case EnemyState.non:
                NonState();
                break;
            case EnemyState.idle:
                destination.enabled = false;
                isIdle = true;
                isWalking = false;
                StartCoroutine(IdleState());
                break;
            case EnemyState.walking:
                destination.enabled = true;
                isWalking = true;
                isIdle = false;
                StartCoroutine(WalkState());
                break;
            case EnemyState.attack:
                StartCoroutine(AttackState());
                break;
            case EnemyState.backOff:
                BackOffState();
                break;
            case EnemyState.damage:
                StartCoroutine(DamageState());
                break;
            case EnemyState.dead:
                DeathState();
                break;

        }

        GetDirFromPlayer();

        if (myHealth <= 0 && !isDead)
        {
            //DropLootAndDie();
            myState = EnemyState.dead;
        } //ded

        if (player == null)
        {
            Destroy(gameObject);
        }

        Anims();
    }

    void NonState()
    {
        if (nonStateChecker)
        {
            StopAllCoroutines();
            nonStateChecker = true;
        }
        if (myRoom.bounds.Contains(player.transform.position))
        {
            myState = EnemyState.idle;
        }
    }

    IEnumerator IdleState()
    {
        StopCoroutine(WalkState());
        if (player == null)
        {
            Destroy(gameObject);
        }
        else if (!myRoom.bounds.Contains(player.transform.position))
        {
            myState = EnemyState.non;
        }

        yield return new WaitForSeconds(2f);
        myState = EnemyState.walking;

        yield return null;
    }

    IEnumerator WalkState()
    {
        StopCoroutine(IdleState());
        StopCoroutine(AttackState());
        //StopCoroutine(IdleState());
        //yield return new WaitForSeconds(Random.Range(0, 3));
        aIPath.enabled = true;
        aIPath.canMove = true;
        destination.target = player.transform;
        aIPath.maxSpeed = speed;
        while (myRoom.bounds.Contains(player.transform.position)) //Go to player
        {
            Collider2D overlap = Physics2D.OverlapCircle(transform.position, overlapRange, mask);
            //Debug.Log("player is in room");
            yield return new WaitForSeconds(.01f);
            /*walkPoint = (player.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector2(walkPoint.x, walkPoint.y);*/
            if (aIPath.reachedEndOfPath && overlap)
            {
                //Debug.Log("Player is here, do attack");
                aIPath.enabled = false;
                //yield return new WaitForSeconds(2f);
                myState = EnemyState.attack;
            }
        }
        if (!myRoom.bounds.Contains(player.transform.position)) //Return to startPos
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

        //Debug.Log("Walkstate is fin");
        aIPath.canMove = false;
        myState = EnemyState.idle;

        yield return null;
    }

    IEnumerator AttackState()
    {
        StopCoroutine(WalkState());
        anim.SetTrigger("DoAttack");
        yield return new WaitForSeconds(.5f);
        anim.ResetTrigger("DoAttack");
        isAttacking = true;
        aIPath.canMove = false;
        aIPath.enabled = false;
        if (!hasAttacked)
        {
            InstanstiateAttack();
            hasAttacked = true;
        }
        yield return new WaitForSeconds(.5f);
        myState = EnemyState.backOff;
        yield return null;
    }

    void InstanstiateAttack()
    {
        GameObject attackClone = Instantiate(attackPrefab, transform.position, Quaternion.Euler(0, 0, dirRotation), transform); //Add an attackPoint
        Destroy(attackClone, 0.3f);
    }

    void BackOffState()
    {
            destination.enabled = true;
            aIPath.enabled = true;
            aIPath.canMove = true;
        if (backOffCounter == 0)
        {
            StopAllCoroutines();
            if (direction == "U")
            {
                destination.target = pointC;
            }
            else if (direction == "D")
            {
                destination.target = pointD;
            }
            else if (direction == "L")
            {
                destination.target = pointB;
            }
            else if (direction == "R")
            {
                destination.target = pointA;
            }

            //aIPath.maxSpeed = speed * 2;


            
            Invoke("BackToWalk", 2f);
            backOffCounter = 1;
        }
    }

    void BackToWalk()
    {
        hasAttacked = false;
        myState = EnemyState.walking;
        backOffCounter = 0;
    }

    IEnumerator DamageState()
    {
        anim.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(.1f);
        anim.ResetTrigger("TakeDamage");
        aIPath.enabled = false;
        destination.enabled = false;
        isHurt = true;
        yield return new WaitForSeconds(.9f);
        myState = EnemyState.walking;
        yield return null;
    }

    void DeathState()
    {
        rb.bodyType = RigidbodyType2D.Static;
        StopAllCoroutines();
        isDead = true;
        aIPath.enabled = false;
        destination.enabled = false;
        anim.SetTrigger("Dead");
        GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
        //DropLootAndDie();
        if (!dropping)
        {
            dropping = true;
            Invoke("DropLootAndDie", 2f);
        }
    }

    void DamagePopUp(float damage, bool crit)
    {
        float fadetime = 0.7f;

        double dmgprint = System.Math.Round(damage, 2);
        if (!crit)
        {
            GameObject dmgpopupclone = Instantiate(damagePopup, transform.position + transform.up * 15, Quaternion.identity);
            dmgtext = dmgpopupclone.GetComponentInChildren<TextMeshProUGUI>();
            dmgtext.CrossFadeAlpha(0, fadetime, false);
            dmgtext.text = dmgprint.ToString();
            dmgpopupclone.SetActive(true);
            Destroy(dmgpopupclone, fadetime);
        }
        else
        {
            GameObject dmgpopupclone = Instantiate(damagePopup, transform.position + transform.up * 18, Quaternion.identity);
            dmgtext = dmgpopupclone.GetComponentInChildren<TextMeshProUGUI>();
            dmgtext.color = Color.red;
            dmgtext.fontSize = 42;
            dmgtext.CrossFadeAlpha(0, fadetime, false);
            dmgtext.text = dmgprint.ToString();
            dmgpopupclone.SetActive(true);
            Destroy(dmgpopupclone, fadetime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Sword" && !isDead || collider.tag == "Arrow" && !isDead || collider.tag == "SwordProjectile" && !isDead)
        {
            if (attacked)
            {
                return;
            }
            PlayerStats playerstats = player.GetComponent<PlayerStats>();
            Player rue = player.GetComponent<Player>();
            Arrow arrowCrit = collider.GetComponent<Arrow>();
            Swordscript swordCrit = collider.GetComponent<Swordscript>();
            SwordProjectile swordProjectileCrit = collider.GetComponent<SwordProjectile>();
            float str = playerstats.Strength.Value;
            float dex = playerstats.Dexterity.Value;
            float critdmg = playerstats.CritDamage.Value / 100;
            float crithpdmg = playerstats.PercentHpDmg.Value;
            float ruehpdmg = playerstats.RueHPDmgOnHit.Value;
            float bowmod = playerstats.CrossbowAttackModifier.Value;
            float swordmod = playerstats.SwordAttackModifier.Value;
            float rapidfire = playerstats.RapidFire.Value;
            float firearrow = playerstats.FireArrows.Value;
            float swordexecute = playerstats.SwordExecute.Value;
            bool crit = false;



            myState = EnemyState.damage;
            if (collider.tag == "Arrow")
            {
                float damage = 0;
                if (rapidfire > 0)
                {
                    damage = ((str / 10) + dex) * bowmod;
                }
                if (rapidfire == 0)
                {
                    damage = str * bowmod;
                }
                if (firearrow > 0)
                {
                    damage *= 2;
                }
                if (arrowCrit.crit && crithpdmg == 0)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (arrowCrit.crit && crithpdmg > 0)
                {
                    damage = (damage * critdmg) + (EnemyStats.health * 0.02f);
                    crit = true;
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }
                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);

                attacked = true;
            }
            else if (collider.tag == "Sword")
            {
                float damage = str * swordmod;
                if (swordexecute > 0 && myHealth <= (EnemyStats.health * 0.2))
                {
                    damage = myHealth;
                }
                if (swordCrit.Crit && crithpdmg == 0)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (swordCrit.Crit && crithpdmg > 0)
                {
                    damage = (damage * critdmg) + (EnemyStats.health * 0.02f);
                    crit = true;
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }
                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                attacked = true;
            }
            else if (collider.tag == "SwordProjectile")
            {
                float damage = str * swordmod;
                if (swordexecute > 0 && myHealth <= (EnemyStats.health * 0.2))
                {
                    damage = myHealth;
                }
                if (swordProjectileCrit.crit && crithpdmg == 0)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (swordProjectileCrit.crit && crithpdmg > 0)
                {
                    damage = (damage * critdmg) + (EnemyStats.health * 0.02f);
                    crit = true;
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }
                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                attacked = true;
            }
        }
    } //Deal damage to enemy

    void DropLootAndDie()
    {
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
        if (playerstats.DropGarantueed.Value == 0)
        {
            foreach (var item in Table) //checks table
            {
                lootTotal += item;
            }
            float randomNumber = Random.Range(0, (lootTotal + 1)); //pulls random number based on table total + 1


            foreach (var weight in Table) //weight, is the number listed in the table of drop chance.
            {
                if (randomNumber <= weight) //if less or equal to a weight, give item
                {


                    if (weight == commondropRange)
                    {
                        Instantiate<GameObject>(commonLoot, transform.position, Quaternion.identity);
                        Corpse();
                        return;
                    }

                    if (weight == raredropRange)
                    {
                        Instantiate<GameObject>(rareLoot, transform.position, Quaternion.identity);
                        Corpse();
                        return;
                    }

                    if (weight == legendarydropRange)
                    {
                        Instantiate<GameObject>(legendaryLoot, transform.position, Quaternion.identity);
                        Corpse();
                        return;
                    }

                    if (weight == ancientdropRange)
                    {
                        Instantiate<GameObject>(ancientLoot, transform.position, Quaternion.identity);
                        Corpse();
                        return;
                    }

                    if (weight == potiondropRange)
                    {
                        Instantiate<GameObject>(potion, transform.position, Quaternion.identity);
                        Corpse();
                        return;
                    }

                    if (weight == none)
                    {
                        Corpse();
                        return;
                    }
                }

                else //if not, roll -= highest value weight.

                {
                    randomNumber -= weight;
                }
            }
        }

        if (playerstats.DropGarantueed.Value > 0)
        {
            float randomNumberDrop = Random.Range(0, 2);
            if (randomNumberDrop == 0)
            {
                float randomnumberLoot = Random.Range(0, 4);
                if (randomnumberLoot == 0)
                {
                    Instantiate<GameObject>(commonLoot, transform.position, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 1)
                {
                    Instantiate<GameObject>(rareLoot, transform.position, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 2)
                {
                    Instantiate<GameObject>(legendaryLoot, transform.position, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 3)
                {
                    Instantiate<GameObject>(ancientLoot, transform.position, Quaternion.identity);
                    Corpse();
                    return;
                }
            }
            if (randomNumberDrop == 1)
            {
                float randomnumberLoot = Random.Range(0, 4);
                if (randomnumberLoot == 0)
                {
                    Instantiate<GameObject>(commonLoot, transform.position, Quaternion.identity);
                    Instantiate<GameObject>(potion, transform.position + transform.right * 10, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 1)
                {
                    Instantiate<GameObject>(rareLoot, transform.position, Quaternion.identity);
                    Instantiate<GameObject>(potion, transform.position + transform.right * 10, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 2)
                {
                    Instantiate<GameObject>(legendaryLoot, transform.position, Quaternion.identity);
                    Instantiate<GameObject>(potion, transform.position + transform.right * 10, Quaternion.identity);
                    Corpse();
                    return;
                }
                if (randomnumberLoot == 3)
                {
                    Instantiate<GameObject>(ancientLoot, transform.position, Quaternion.identity);
                    Instantiate<GameObject>(potion, transform.position + transform.right * 10, Quaternion.identity);
                    Corpse();
                    return;
                }
            }
        }
    }
    public void Corpse()
    {
        Destroy(destroyChildren);
        GetComponentInChildren<Animator>().enabled = false;
        transform.DetachChildren();
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, overlapRange);
    }
}
