using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class JBoss : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public enum BossState { idle, walk, decide, melee, ranged, telegraph, attack, dead, stagger };
    public BossState myState = BossState.idle;

    AIPath path;
    AIDestinationSetter destination;

    GameObject player;

    Animator anim;
    SpriteRenderer myRend;
    BossSound mySound;

    bool isDead, telegraphed, isAttacking;
    public bool doShake, doScream, doCrumble, spawnPattern;

    string direction;

    int dirRotation;

    int attackState;

    public Vector2 shakeAmp, shakeFreq;

    public GameObject[] bulletPatterns;
    public List<GameObject> patternsInScene = new List<GameObject>();
    public Transform patternPoint;

    public GameObject damagePopup;
    public TextMeshProUGUI dmgtext;

    public LayerMask playerMask, teleRangeMask;
    public float overlapRange;
    public Transform teleRangePoint;

    #region Stat based variables
    public int level;
    public EnemyStats EnemyStats;
    public EnemyType thisType;
    public float speed = 30f;
    public float myHealth;
    public float debugHealth;
    public float damage;
    public HealthSystem healthSystem;
    public bool attacked, arrowKnockback, swordprojectileattacked;

    //loot
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

    void Awake()
    {
        spawnPattern = false;
        isAttacking = false;
        telegraphed = false;
        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        myRend = GetComponentInChildren<SpriteRenderer>();

        path = GetComponent<AIPath>();
        destination = GetComponent<AIDestinationSetter>();
        mySound = GetComponent<BossSound>();

        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        //StartBoss();

    }
    public void StartBoss()
    {
        doShake = false;
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

        path.maxSpeed = speed;
    }

    void Update()
    {
        if (myHealth <= 0 && !isDead)
        {
            if (patternsInScene.Count > 0)
            {
                Destroy(patternsInScene[0]);
                patternsInScene.Remove(patternsInScene[0]);
            }
            mySound.PlayDeathSound();
            //DropLootAndDie();
            myState = BossState.dead;
            isDead = true;
        } //ded

        switch (myState)
        {
            case BossState.idle:
                isAttacking = false;
                telegraphed = false;
                path.enabled = false;
                overlapRange = 40f;
                StartCoroutine(IdleState());
                break;
            case BossState.walk:
                path.enabled = true;
                StartCoroutine(WalkState());
                break;
            case BossState.decide:
                DecideState();
                break;
            case BossState.melee:
                path.enabled = false;
                StartCoroutine(MeleeState());
                break;
            case BossState.ranged:
                path.enabled = false;
                StartCoroutine(RangedState());
                break;
            case BossState.telegraph:
                StartCoroutine(TelegraphState());
                break;
            case BossState.attack:
                break;
            case BossState.dead:
                isAttacking = false;
                telegraphed = false;
                path.enabled = false;
                StopAllCoroutines();
                DeadState();
                break;
            case BossState.stagger:
                break;
        }

        myHealth = healthSystem.GetHealth();
        attacked = false;
        Anims();
        CheckForOtherSounds();
        GetDirFromPlayer();
        if (spawnPattern && patternsInScene.Count < 1)
        {
            InstansiatePattern();
            spawnPattern = false;
        }

        if (patternsInScene[0] == null)
        {
            patternsInScene.Remove(patternsInScene[0]);
        }

    }

    #region States
    void DeadState()
    {
        anim.SetTrigger("IsDead");
        this.enabled = false;
    }

    IEnumerator IdleState()
    {
        spawnPattern = false;
        Debug.Log("Idle");
        yield return new WaitForSeconds(1f);
        myState = BossState.walk;
        StopAllCoroutines();
    }

    IEnumerator WalkState()
    {
        path.enabled = true;
        Debug.Log("Walk");
        yield return new WaitForSeconds(2f);
        myState = BossState.decide;
        StopAllCoroutines();
    }

    void DecideState()
    {
        StopAllCoroutines();
        //attackState = 2;
        //telegraphed = true;
        //myState = BossState.telegraph;
        int rand = Random.Range(0, 2);
        int tele = Random.Range(0, 2);
        if (rand < 1)
        {
            attackState = 1;
            if (tele < 1)
            {
                //telegraphed = true;
                myState = BossState.telegraph;
            }
            else
            {
                telegraphed = false;
                Collider2D overlap = Physics2D.OverlapCircle(transform.position, overlapRange, playerMask);
                overlapRange = 40f;
                if (overlap)
                {
                    myState = BossState.melee;
                }
                else
                {
                    attackState = 2;
                    myState = BossState.ranged;
                }
            }
        }
        else
        {
            attackState = 2;
            if (tele < 1)
            {
                //telegraphed = true;
                myState = BossState.telegraph;
            }
            else
            {
                telegraphed = false;
                Collider2D overlap = Physics2D.OverlapCircle(transform.position, overlapRange, playerMask);
                overlapRange = 40f;

                if (overlap)
                {
                    attackState = 1;
                    myState = BossState.melee;
                }
                else
                {
                    attackState = 2;
                    myState = BossState.ranged;
                }
            }
        }
    }

    IEnumerator MeleeState()
    {
        telegraphed = false;
        isAttacking = true;
        Debug.Log("Melee");
        yield return new WaitForSeconds(1f);
        myState = BossState.idle;
        StopAllCoroutines();
    }

    IEnumerator RangedState()
    {
        telegraphed = false;
        isAttacking = true;
        Debug.Log("Ranged");
        yield return new WaitForSeconds(1f);
        myState = BossState.idle;
        StopAllCoroutines();
    }
    IEnumerator TelegraphState()
    {
        if (attackState == 1)
        {
            //Melee
            Collider2D overlap = Physics2D.OverlapCircle(transform.position, overlapRange, playerMask);
            overlapRange = 40f;
            destination.target = player.transform;
            while (!overlap)
            {
                yield return new WaitForSeconds(.1f);
            }
            path.enabled = false;
            telegraphed = true;
            yield return new WaitForSeconds(3.9f);
            path.enabled = true;
            myState = BossState.idle;
        }
        else if (attackState == 2)
        {
            //Ranged
            Collider2D overlap = Physics2D.OverlapCircle(transform.position, overlapRange, teleRangeMask);
            overlapRange = 15f;
            destination.target = teleRangePoint;
            while (!overlap)
            {
                yield return new WaitForSeconds(.1f);
            }
            telegraphed = true;
            path.enabled = false;
            yield return new WaitForSeconds(5.4f);
            destination.target = player.transform;
            path.enabled = true;
            myState = BossState.idle;
        }
        StopAllCoroutines();
    }
    #endregion

    public void InstansiatePattern()
    {
        GameObject patternClone = Instantiate(bulletPatterns[0], patternPoint.position, Quaternion.identity);
        patternsInScene.Add(patternClone);
    }

    void GetDirFromPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        int dirTwo = Mathf.RoundToInt(dir.x);
        int dirThree = Mathf.RoundToInt(dir.y);
        Vector2 dirVector = new Vector2(dirTwo, dirThree);
        //Debug.Log("OG number = " + dir.x + " normalized number = " + dirTwo + ", " + "OG number = " + dir.y + " normalized number = " + + dirThree);
        //Debug.Log("x: " + dirTwo + " y: " + dirThree);
        if (myState != BossState.attack && !isDead)
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
            else if (dirVector == new Vector2(1f, 0f) && myState == BossState.walk)
            {
                direction = "R";
                dirRotation = 270;
            }
            else if (dirVector == new Vector2(-1f, 0f) && myState == BossState.walk)
            {
                direction = "L";
                dirRotation = 90;
            }
        }
    }

    void Anims()
    {
        #region Not directly animator
        if (direction == "L")
        {
            myRend.sortingOrder = 149;
            Transform animScale = anim.gameObject.transform;
            animScale.localScale = new Vector3(-1, 1, 1);
        }
        if (direction == "R")
        {
            myRend.sortingOrder = 149;
            Transform animScale = anim.gameObject.transform;
            animScale.localScale = new Vector3(1, 1, 1);
        }
        if (direction == "U")
        {
            myRend.sortingOrder = 151;
        }
        if (direction == "D")
        {
            myRend.sortingOrder = 149;
        }
        #endregion

        if (myState == BossState.walk)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        if (myState == BossState.idle)
        {
            anim.SetBool("IsIdle", true);
        }
        else
        {
            anim.SetBool("IsIdle", false);
        }

        anim.SetBool("Telegraphed", telegraphed);
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetInteger("AttackState", attackState);

        CheckForShake();
        CheckForScream();
    }


    void CheckForShake()
    {
        if (doShake)
        {
            StartCoroutine(CamShake());
            doShake = false;
        }
    }

    IEnumerator CamShake()
    {
        //Starts cam shake
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Random.Range(shakeAmp.x, shakeAmp.y);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Random.Range(shakeFreq.x, shakeFreq.y);
        yield return new WaitForSeconds(.3f);
        //Resets cam shake
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        myState = BossState.idle;
        StopCoroutine(CamShake());
    }

    public void CheckForScream()
    {
        if (doScream && attackState == 1)
        {
            mySound.PlayMeleeScream();
        }
        else if (doScream && attackState == 2)
        {
            mySound.PlayRangedScream();
        }
    }

    public void CheckForOtherSounds()
    {
        if (doCrumble)
        {
            mySound.CrumbleSound();
            doCrumble = false;
        }
    }

    #region Damage to boss
    void DamagePopUp(float damage, bool crit)
    {
        float fadetime = 0.7f;

        double dmgprint = System.Math.Round(damage, 2);
        if (!crit)
        {
            mySound.PlayDamageSound();
            GameObject dmgpopupclone = Instantiate(damagePopup, transform.position + transform.up * 15, Quaternion.identity);
            dmgtext = dmgpopupclone.GetComponentInChildren<TextMeshProUGUI>();
            dmgpopupclone.AddComponent<Rigidbody2D>();
            dmgpopupclone.GetComponent<Rigidbody2D>().velocity = RandomVector(-10f, 10f);
            dmgtext.CrossFadeAlpha(0, fadetime, false);
            dmgtext.text = dmgprint.ToString();
            dmgpopupclone.SetActive(true);
            Destroy(dmgpopupclone, fadetime);
        }
        else
        {
            mySound.PlayDamageSound();
            GameObject dmgpopupclone = Instantiate(damagePopup, transform.position + transform.up * 18, Quaternion.identity);
            dmgtext = dmgpopupclone.GetComponentInChildren<TextMeshProUGUI>();
            dmgpopupclone.AddComponent<Rigidbody2D>();
            dmgpopupclone.GetComponent<Rigidbody2D>().velocity = RandomVector(-20f, 20f);
            dmgtext.color = Color.red;
            dmgtext.fontSize = 42;
            dmgtext.CrossFadeAlpha(0, fadetime, false);
            dmgtext.text = dmgprint.ToString();
            dmgpopupclone.SetActive(true);
            Destroy(dmgpopupclone, fadetime);
        }
    }

    Vector3 RandomVector(float min, float max)
    {
        float x = UnityEngine.Random.Range(min, max);
        float y = 10;
        float z = 0;
        return new Vector3(x, y, z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Sword" && !isDead || collider.tag == "Arrow" && !isDead || collider.tag == "SwordProjectile" && !isDead)
        {

            PlayerStats playerstats = player.GetComponent<PlayerStats>();
            Player rue = player.GetComponent<Player>();
            Arrow arrowCrit = collider.GetComponent<Arrow>();
            Swordscript swordCrit = collider.GetComponent<Swordscript>();
            SwordProjectile swordProjectileCrit = collider.GetComponent<SwordProjectile>();
            float str = playerstats.Strength.Value;
            float dex = playerstats.Dexterity.Value;
            float critdmg = playerstats.CritDamage.Value / 100;
            float currenthpdmg = playerstats.PercentHpDmg.Value;
            float ruehpdmg = playerstats.RueHPDmgOnHit.Value;
            float bowmod = playerstats.CrossbowAttackModifier.Value;
            float swordmod = playerstats.SwordAttackModifier.Value;
            float rapidfire = playerstats.RapidFire.Value;
            float firearrow = playerstats.FireArrows.Value;
            float swordexecute = playerstats.SwordExecute.Value;
            bool crit = false;




            if (collider.tag == "Arrow")
            {
                if (attacked)
                {
                    return;
                }
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
                if (arrowCrit.crit)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (currenthpdmg > 0)
                {
                    damage = damage + (myHealth * 0.1f);
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }


                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);

                if (crit)
                {
                    myState = BossState.stagger;
                }
                attacked = true;

                if (playerstats.ArrowKnockback.Value < 0)
                {
                    arrowKnockback = false;
                }
                else
                {

                }

            }
            else if (collider.tag == "Sword")
            {
                if (attacked)
                {
                    return;
                }
                float damage = str * swordmod;
                if (swordexecute > 0 && myHealth <= (EnemyStats.health * 0.2))
                {
                    damage = myHealth;
                }
                if (swordCrit.Crit)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (currenthpdmg > 0)
                {
                    damage = damage + (myHealth * 0.1f);
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }
                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);
                if (crit)
                {
                    myState = BossState.stagger;
                }
                attacked = true;
            }
            else if (collider.tag == "SwordProjectile")
            {
                if (swordprojectileattacked)
                {
                    return;
                }
                float damage = str * swordmod;
                if (swordexecute > 0 && myHealth <= (EnemyStats.health * 0.2))
                {
                    damage = myHealth;
                }
                if (swordProjectileCrit.crit)
                {
                    damage *= critdmg;
                    crit = true;
                }
                if (currenthpdmg > 0)
                {
                    damage = damage + (myHealth * 0.1f);
                }
                if (ruehpdmg > 0)
                {
                    damage += (playerstats.Health.Value * 0.1f);
                }
                DamagePopUp(damage, crit);
                healthSystem.Damage(damage);
                rue.HealthSystem.Heal(playerstats.LifeOnHit.Value);

                if (crit)
                {
                    myState = BossState.stagger;
                }
                swordprojectileattacked = true;
            }
        }
    } //Deal damage to enemy
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, overlapRange);
    }
}
