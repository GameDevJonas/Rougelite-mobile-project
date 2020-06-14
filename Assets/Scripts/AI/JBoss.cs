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

    bool isDead;
    public bool doShake, doScream, doCrumble;

    string direction;

    int dirRotation;

    public Vector2 shakeAmp, shakeFreq;

    void Awake()
    {
        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        myRend = GetComponentInChildren<SpriteRenderer>();

        path = GetComponent<AIPath>();
        destination = GetComponent<AIDestinationSetter>();
        mySound = GetComponent<BossSound>();

    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(25, 25, 0);
        GetDirFromPlayer();
        Anims();
        CheckForOtherSounds();



        switch (myState)
        {
            case BossState.idle:
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
                MeleeState();
                break;
            case BossState.ranged:
                RangedState();
                break;
            case BossState.telegraph:
                TelegraphState();
                break;
            case BossState.attack:
                break;
            case BossState.dead:
                path.enabled = false;
                break;
        }
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

        CheckForShake();
        CheckForScream();
    }

    IEnumerator IdleState()
    {
        Debug.Log("Idle");
        yield return new WaitForSeconds(1f);
        myState = BossState.walk;
    }

    IEnumerator WalkState()
    {
        Debug.Log("Walk");
        yield return new WaitForSeconds(2f);
        myState = BossState.decide;
    }

    void DecideState()
    {
        StopAllCoroutines();
        Debug.Log("Decide");
        myState = BossState.melee;
    }

    public void CheckForScream()
    {
        if (doScream)
        {
            mySound.PlayMeleeScream();
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

    void MeleeState()
    {
        Debug.Log("Melee");
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
    }

    void RangedState()
    {
        Debug.Log("Ranged");
    }

    void TelegraphState()
    {
        Debug.Log("Telegraph");
    }
}
