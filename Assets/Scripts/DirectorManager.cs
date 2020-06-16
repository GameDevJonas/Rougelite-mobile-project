using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Pathfinding;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class DirectorManager : MonoBehaviour
{
    PlayableDirector director;
    public PlayableAsset newClip;
    double time;
    float timer;
    bool initChecker = false;

    public AudioSource music, ambience;
    public AudioClip bossLoop;

    CinemachineVirtualCamera vCam;

    public GameObject belial;
    Player player;

    public GameObject canvasToDestroy;

    GameObject altar;

    void Awake()
    {
        if (belial != null)
        {
            music.clip = null;
            ambience.enabled = false;
            vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            vCam.m_Lens.OrthographicSize = 60;
            vCam.GetComponent<BossCamera>().enabled = false;
            //vCam.Follow = belial.transform;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            director = GetComponent<PlayableDirector>();
            time = director.duration;
            belial.GetComponentInChildren<Light2D>().intensity = 1;
            belial.GetComponent<AIPath>().enabled = false;
            belial.GetComponent<JBoss>().enabled = false;
            belial.GetComponent<AIDestinationSetter>().enabled = false;
            player.GetComponentInChildren<CanvasGroup>().GetComponent<Animator>().enabled = true;
            player.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            player.enabled = false;
            altar = FindObjectOfType<FromBossToDungeon>().gameObject;
            altar.SetActive(false);
            belial.GetComponent<JBoss>().altar = altar;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            player.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= time && !initChecker)
        {
            Destroy(canvasToDestroy);
            player.GetComponentInChildren<Canvas>().GetComponent<CanvasGroup>().enabled = false;
            music.Stop();
            music.clip = bossLoop;
            music.Play();
            ambience.enabled = true;
            belial.GetComponentInChildren<Light2D>().intensity = 0;
            vCam.GetComponent<BossCamera>().enabled = true;
            player.enabled = true;
            belial.GetComponent<AIDestinationSetter>().enabled = true;
            belial.GetComponent<AIPath>().enabled = true;
            belial.GetComponent<JBoss>().enabled = true;
            belial.GetComponent<JBoss>().StartBoss();
            initChecker = true;
            //this.enabled = false;
        }
        else if (!initChecker)
        {
            timer += Time.deltaTime;
        }
    }

    public void EndBoss()
    {
        director.playableAsset = newClip;
        director.Play();
    }

}
