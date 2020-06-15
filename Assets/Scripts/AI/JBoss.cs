using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using Cinemachine;

public class JBoss : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public enum BossState { idle, walk, decide, melee, ranged, telegraph, attack, dead };
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

    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case BossState.idle:
                isAttacking = false;
                telegraphed = false;
                StartCoroutine(IdleState());
                path.enabled = false;
                break;
            case BossState.walk:
                StartCoroutine(WalkState());
                path.enabled = true;
                break;
            case BossState.decide:
                DecideState();
                path.enabled = false;
                break;
            case BossState.melee:
                StartCoroutine(MeleeState());
                break;
            case BossState.ranged:
                StartCoroutine(RangedState());
                break;
            case BossState.telegraph:
                StartCoroutine(TelegraphState());
                break;
            case BossState.attack:
                break;
            case BossState.dead:
                path.enabled = false;
                StopAllCoroutines();
                break;
        }

        //transform.localScale = new Vector3(25, 25, 0);
        Anims();
        CheckForOtherSounds();
        GetDirFromPlayer();
        if (spawnPattern && patternsInScene.Count < 1)
        {
            InstansiatePattern();
            spawnPattern = false;
        }

        if(patternsInScene[0] == null)
        {
            patternsInScene.Remove(patternsInScene[0]);
        }

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
        Debug.Log("Walk");
        yield return new WaitForSeconds(2f);
        myState = BossState.decide;
        StopAllCoroutines();
    }

    void DecideState()
    {
        StopAllCoroutines();
        attackState = 2;
        telegraphed = true;
        myState = BossState.telegraph;
        int rand = Random.Range(0, 2);
        int tele = Random.Range(0, 2);
        if (rand < 1)
        {
            attackState = 1;
            if (tele < 1)
            {
                telegraphed = true;
                myState = BossState.telegraph;
            }
            else
            {
                telegraphed = false;
                myState = BossState.melee;
            }
        }
        else
        {
            attackState = 2;
            if (tele < 1)
            {
                telegraphed = true;
                myState = BossState.telegraph;
            }
            else
            {
                telegraphed = false;
                myState = BossState.ranged;
            }
        }
    }

    IEnumerator MeleeState()
    {
        telegraphed = false;
        isAttacking = true;
        Debug.Log("Melee");
        yield return new WaitForSeconds(2f);
        myState = BossState.idle;
        StopAllCoroutines();
    }

    IEnumerator RangedState()
    {
        telegraphed = false;
        isAttacking = true;
        Debug.Log("Ranged");
        yield return new WaitForSeconds(2f);
        myState = BossState.idle;
        StopAllCoroutines();
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

    IEnumerator TelegraphState()
    {
        if (attackState == 1)
        {
            //Melee
            yield return new WaitForSeconds(3.9f);
            myState = BossState.idle;
        }
        else if (attackState == 2)
        {
            //Ranged
            yield return new WaitForSeconds(5.4f);
            myState = BossState.idle;
        }
        StopAllCoroutines();
    }

    public void InstansiatePattern()
    {
        GameObject patternClone = Instantiate(bulletPatterns[0], patternPoint.position, Quaternion.identity);
        patternsInScene.Add(patternClone);
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
        else if(doScream && attackState == 2)
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



}
