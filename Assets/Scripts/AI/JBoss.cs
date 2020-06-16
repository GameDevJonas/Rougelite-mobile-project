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

    public AIPath path;
    public AIDestinationSetter destination;

    GameObject player;

    Animator anim;
    SpriteRenderer myRend;
    BossSound mySound;

    bool isDead, telegraphed, isAttacking;
    public bool doShake, doScream, doCrumble, spawnPattern, spawnCheapMelee, spawnCheapRange, spawnMeleeTele, inStagger;

    public string direction;

    int dirRotation;

    public int attackState;

    public Vector2 shakeAmp, shakeFreq;

    public GameObject damagePopup;
    public TextMeshProUGUI dmgtext;

    public Transform shootPoint;
    public GameObject bullet;

    public GameObject cheapMelee;

    public GameObject teleMelee;

    public GameObject destroyChildren;

    public GameObject[] bulletPatterns;
    public List<GameObject> patternsInScene = new List<GameObject>();
    public Transform patternPoint;

    public LayerMask playerMask, teleRangeMask;
    public float overlapRange;
    public Transform teleRangePoint;

    public GameObject altar;

    public int counter;

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

        //StartBoss();

    }

    private void Start()
    {
        //altar.SetActive(false);
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
    }


    public void StartBoss()
    {

        if (path == null)
        {
            path = GetComponent<AIPath>();
        }
        if (destination == null)
        {
            destination = GetComponent<AIDestinationSetter>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
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
                counter = 0;
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
                destination.enabled = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                StopAllCoroutines();
                DeadState();
                break;
            case BossState.stagger:
                StaggerState();
                break;
        }

        myHealth = healthSystem.GetHealth();
        attacked = false;
        Anims();

        GetDirFromPlayer();
        if (spawnPattern && patternsInScene.Count < 1)
        {
            InstansiatePattern();
            spawnPattern = false;
        }
        InstantiateCheapMelee();
        InstantiateCheapRanged();
        InstantiateMeleeTele();



        if (patternsInScene.Count > 0 && patternsInScene[0] == null)
        {
            patternsInScene.Remove(patternsInScene[0]);
        }
    }

    #region States
    void StaggerState()
    {
        StopAllCoroutines();
        inStagger = true;
        path.enabled = false;
        destination.enabled = false;
        Invoke("OutOfStagger", .2f);
    }

    void OutOfStagger()
    {
        inStagger = false;
        path.enabled = true;
        destination.enabled = true;
        myState = BossState.walk;
    }

    void DeadState()
    {
        BossCamera script = FindObjectOfType<BossCamera>();
        script.enabled = false;
        vCam.Follow = gameObject.transform;
        player.GetComponent<Player>().enabled = false;
        if (!dropping)
        {
            dropping = true;
            FindObjectOfType<DirectorManager>().EndBoss();
            if (SceneManager.GetActiveScene().buildIndex != 10)
            {
                Invoke("DropLootAndDie", 2f);
            }
            else
            {
                Invoke("StopDeathBelial", 2f);
                Invoke("Corpse", 2f);
            }
        }
    }

    void StopDeathBelial()
    {
        anim.SetBool("StopDeath", true);
    }

    IEnumerator IdleState()
    {
        spawnPattern = false;
        yield return new WaitForSeconds(1f);
        myState = BossState.walk;
        StopAllCoroutines();
    }

    IEnumerator WalkState()
    {
        path.enabled = true;
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
        //Instantiate melee swing in front/towards player
        yield return new WaitForSeconds(1f);
        myState = BossState.idle;
        StopAllCoroutines();
    }

    IEnumerator RangedState()
    {
        telegraphed = false;
        isAttacking = true;
        //Instantiate bullet from shootPoint
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
                overlapRange = 40f;
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
            destination.target = teleRangePoint;
            overlapRange = 15f;
            int count = 0;
            while (!overlap)
            {
                overlapRange = 15f;
                yield return new WaitForSeconds(.1f);
                count++;
                if (count >= 20)
                {
                    myState = BossState.idle;
                }
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

    #region Attack Instantiating
    void InstantiateCheapMelee()
    {
        if (spawnCheapMelee && counter < 1)
        {
            counter++;
            spawnCheapMelee = false;
            mySound.PlayAttackSound();
            GameObject meleeCheapClone = Instantiate(cheapMelee, transform.position, Quaternion.Euler(0, 0, dirRotation), transform);
            meleeCheapClone.transform.localScale = new Vector3(400, 400, 1);
            Destroy(meleeCheapClone, .3f);
        }
    }

    void InstantiateCheapRanged()
    {
        if (spawnCheapRange && counter < 1)
        {
            counter++;
            spawnCheapRange = false;
            float rotationToPlayer = (player.transform.position - transform.position).magnitude;
            GameObject bulletClone = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            bulletClone.GetComponent<Bullet>().bulletForce = 6000;
            bulletClone.transform.up = player.transform.position - transform.position;
            Destroy(bulletClone, 3f);
        }
    }

    void InstantiateMeleeTele()
    {
        if (spawnMeleeTele && counter < 1)
        {
            counter++;
            spawnMeleeTele = false;
            GameObject meleeTeleClone = Instantiate(teleMelee, patternPoint.position, Quaternion.Euler(0, 0, -180), transform);
            meleeTeleClone.transform.localScale = new Vector3(630, 740, 1);
            Destroy(meleeTeleClone, .5f);
        }
    }

    public void InstansiatePattern()
    {
        GameObject patternClone = Instantiate(bulletPatterns[0], patternPoint.position, Quaternion.identity);
        patternsInScene.Add(patternClone);
    }
    #endregion

    void GetDirFromPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        int dirTwo = Mathf.RoundToInt(dir.x);
        int dirThree = Mathf.RoundToInt(dir.y);
        Vector2 dirVector = new Vector2(dirTwo, dirThree);
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
        anim.SetBool("InStagger", inStagger);
        anim.SetBool("IsDead", isDead);

        CheckForShake();
        CheckForScream();
        CheckForOtherSounds();
    }


    void CheckForShake()
    {
        if (doShake)
        {
            doShake = false;
            StartCoroutine(CamShake());
        }
    }

    IEnumerator CamShake()
    {
        mySound.CrumbleSound();
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
            doCrumble = false;
            //mySound.CrumbleSound();
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

    void DropLootAndDie()
    {
        anim.SetBool("StopDeath", true);
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
        isDead = true;
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
        altar.SetActive(true);
        Invoke("DestroyCorpse", 3f);
    }

    void DestroyCorpse()
    {
        
        vCam.Follow = player.transform;
        player.GetComponent<Player>().enabled = true;
        Destroy(destroyChildren);
        GetComponent<Animator>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        transform.DetachChildren();
        Destroy(gameObject);
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
